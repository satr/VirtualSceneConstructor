using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    internal class StageFileSystemDataAdapter : XmlArchiveDataAdapter<IStage>
    {
        internal static class ArchiveEntryNames
        {
            public const string Geometry = "Geometry";
            public const string Entry = "Entry";
            public const string EntryType = "EntryType";
            public const string Items = "Items";
        }

        /// <summary>
        /// Save an stage in the file system
        /// </summary>
        /// <param name="entity">The stage to be saved</param>
        public override IActionResult Save(IStage entity)
        {
            var actionResult = new ActionResult<IStage>(Resources.Title_Save_the_stage);
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                actionResult.AddError(Resources.Message_Save_Name_of_the_stage_is_invalid);
                return actionResult;
            }

            Archive(entity, GetArchiveFilePathFor(entity));

            return actionResult;
        }

        public ActionResult<IStage> Load(string name)
        {
            var actionResult = new ActionResult<IStage>(Resources.Title_Load_the_stage);
            var archiveFilePath = GetArchiveFilePathFor(name);
            if (!File.Exists(archiveFilePath))
            {
                actionResult.AddError(Resources.Message_File_with_the_stage_does_not_exists);
                return actionResult;
            }
            actionResult.Value = UnArchiveStage(archiveFilePath, actionResult);
            return actionResult;
        }

        /// <summary>
        /// Get the archive file full path for the stage.
        /// </summary>
        /// <param name="entity">The stage.</param>
        /// <returns>THe path to the archive file.</returns>
        public string GetArchiveFilePathFor(IStage entity)
        {
            return GetArchiveFilePathFor(entity.Name);
        }

        private string GetArchiveFilePathFor(string entityName)
        {
            return Path.Combine(StagesFolderPath, entityName + ".zip");
        }

        private void Archive(IStage stage, string archiveFilePath)
        {
            if(stage == null)
                throw new ArgumentNullException(Resources.Message_Archive_Entity_cannot_be_saved_because_it_is_empty);
            
            CreateDirectoryForFileIfNotExists(archiveFilePath);
            
            using (var fileStream = File.Create(archiveFilePath))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                Archive(stage, archive, ArchiveEntryNames.Entry);
                Archive(new TypeInfo(stage), archive, ArchiveEntryNames.EntryType);
                
                foreach (var item in stage.Items)
                {
                    var itemPath = CreateArchiveEntryPath();
                    Archive(item, archive, CreateEntityPath(itemPath));
                    Archive(new TypeInfo(item), archive, CreateEntityTypePath(itemPath));
                    Archive(item.Geometry, archive, CreateGeometryEntityPath(itemPath));
                    Archive(new TypeInfo(item.Geometry), archive, CreateGeometryEntityEntryPath(itemPath));
                }
            }
        }

        private static string CreateGeometryEntityEntryPath(string itemPath)
        {
            return CombinePath(itemPath, ArchiveEntryNames.Geometry ,ArchiveEntryNames.EntryType);
        }

        private static string CreateGeometryEntityPath(string itemPath)
        {
            return CombinePath(itemPath, ArchiveEntryNames.Geometry, ArchiveEntryNames.Entry);
        }

        private static string CreateEntityTypePath(string itemPath)
        {
            return CombinePath(itemPath, ArchiveEntryNames.EntryType);
        }

        private static string CreateEntityPath(string itemPath)
        {
            return CombinePath(itemPath, ArchiveEntryNames.Entry);
        }

        private static string CreateArchiveEntryPath()
        {
            return CombinePath(ArchiveEntryNames.Items, Guid.NewGuid().ToString());
        }

        private static void CreateDirectoryForFileIfNotExists(string archiveFilePath)
        {
            var archiveFileInfo = new FileInfo(archiveFilePath);
            if (archiveFileInfo.Directory != null && !archiveFileInfo.Directory.Exists)
                archiveFileInfo.Directory.Create();
        }

        private void Archive<T>(T entity, ZipArchive archive, string path)
        {
            var entityEntry = CreateEntry(archive, path);
            using (var stream = entityEntry.Open())
                GetSerializerBy(entity).Serialize(stream, entity);
        }

        private object UnArchive(IArchiveEntry<ZipArchiveEntry> archiveEntry, IActionResult actionResult)
        {
            var entryType = UnArchiveEntryType(archiveEntry, actionResult);
            return UnArchiveEntity(archiveEntry, entryType, actionResult);
        }

        private object UnArchiveEntity(IArchiveEntry<ZipArchiveEntry> archiveEntry, Type entryType, IActionResult actionResult)
        {
            var entryEntry = archiveEntry.Entity;
            if (archiveEntry.Entity == null)
            {
                actionResult.AddError(Resources.Message_Archive_entry_entity_is_empty_for_path_N, archiveEntry.Path);
                return null;
            }
            try
            {
                object obj = null;
                using (var stream = entryEntry.Open())
                {
                    obj = GetSerializerByType(entryType).Deserialize(stream);
                }
                return obj;
            }
            catch (Exception e)
            {
                actionResult.AddError(Resources.Message_Archive_entry_entity_cannot_be_created_N, e.Message);
                return null;
            }
        }

        private Type UnArchiveEntryType(IArchiveEntry<ZipArchiveEntry> archiveEntry, IActionResult actionResult)
        {
            if (archiveEntry.EntityType == null)
            {
                actionResult.AddError(string.Format(Resources.Message_Archive_entry_entity_type_is_empty_for_path_N,archiveEntry.Path));
                return null;
            }
            using (var stream = archiveEntry.EntityType.Open())
            {
                try
                {
                    var entityTypeInfo = (TypeInfo)GetSerializer<TypeInfo>().Deserialize(stream);
                    if (entityTypeInfo != null)
                        return entityTypeInfo.CreateEntityType(true);
                }
                catch (Exception e)
                {
                    actionResult.AddError(Resources.Message_Archive_entry_entity_type_cannot_be_created_N, e.Message);
                }
            }
            return null;
        }

        private static string CombinePath(params string[] entryPathElements)
        {
            if (entryPathElements.Length == 0)
                throw new ArgumentException(Resources.Message_Archive_No_entry_elements_is_defined_in_the_archive_path, "entryPathElements");

            var builder = new StringBuilder();
            foreach (var entryPathElement in entryPathElements.Where(entryPathElement => !string.IsNullOrWhiteSpace(entryPathElement)))
            {
                if(builder.Length > 0)
                    builder.Append(@"\");
                builder.Append(entryPathElement);
            }
            return builder.ToString();
        }

        private IStage UnArchiveStage(string archiveFilePath, IActionResult actionResult)
        {
            CreateDirectoryForFileIfNotExists(archiveFilePath);
            using (var fileStream = File.OpenRead(archiveFilePath))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                var archiveHierarchy = GetArchiveHierarchy(archive.Entries);
                var entity = UnArchive(archiveHierarchy, actionResult);
                ValidateEntity<IStage>(entity, actionResult);
                if (!actionResult.Success)
                    return null;
                var stage = (IStage)entity;
                UnArchiveSceneEntities(archiveHierarchy, stage, actionResult);
                return stage;
            }
        }

        private void UnArchiveSceneEntities(IArchiveEntry<ZipArchiveEntry> archiveHierarchy, IStage stage, IActionResult actionResult)
        {
            foreach (var archiveEntry in archiveHierarchy.Items)
            {
                var sceneEntityActionResult = new ActionResult<ISceneEntity>();
                var entity = UnArchive(archiveEntry, sceneEntityActionResult);
                ValidateEntity<ISceneEntity>(entity, sceneEntityActionResult);
                if (!sceneEntityActionResult.Success)
                    continue;
                var sceneEntity = (ISceneEntity) entity;
                if (archiveEntry.Geometry != null)
                {
                    var geometryEntity = UnArchive(archiveEntry.Geometry, sceneEntityActionResult);
                    ValidateEntity<SceneElement>(geometryEntity, sceneEntityActionResult);
                    if (sceneEntityActionResult.Success)
                        sceneEntity.Geometry = (SceneElement)geometryEntity;
                }
                if (sceneEntityActionResult.Success)
                    stage.Items.Add(sceneEntity);
                actionResult.CombineWith(sceneEntityActionResult);
            }
        }

        private static void ValidateEntity<T>(object entity, IActionResult actionResult)
        {
            if (entity == null || !actionResult.Success) 
                return;
            if (!(entity is T))
                actionResult.AddError(Resources.Message_A_type_N_was_expected_by_the_following_type_was_loaded_M, typeof(T), entity.GetType());
        }

        private static IArchiveEntry<ZipArchiveEntry> GetArchiveHierarchy(IEnumerable<ZipArchiveEntry> entries)
        {
            var builder = new ArchiveHierarchyBuilder<ZipArchiveEntry>();
            foreach (var archiveEntry in entries)
            {
                builder.Add(archiveEntry, archiveEntry.FullName);
            }
            return builder.GetHierarchy();
        }
    }
}