using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    internal class ArchiveManager
    {
        public void PackStage(IStage stage, string archiveFilePath)
        {
            if (stage == null)
                throw new ArgumentNullException(Resources.Message_Archive_Entity_cannot_be_saved_because_it_is_empty);

            CreateDirectoryForFileIfNotExists(archiveFilePath);

            using (var fileStream = File.Create(archiveFilePath))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                Pack(stage, archive, ArchiveEntryNames.Entry);
                Pack(new TypeInfo(stage), archive, ArchiveEntryNames.EntryType);

                foreach (var item in stage.Items)
                {
                    var itemPath = CreateArchiveEntryPath();
                    Pack(item, archive, CreateEntityPath(itemPath));
                    Pack(new TypeInfo(item), archive, CreateEntityTypePath(itemPath));
                    Pack(item.Geometry, archive, CreateGeometryEntityPath(itemPath));
                    Pack(new TypeInfo(item.Geometry), archive, CreateGeometryEntityEntryPath(itemPath));
                }
            }
        }

        private static string CreateGeometryEntityEntryPath(string itemPath)
        {
            return CombinePath(itemPath, ArchiveEntryNames.Geometry, ArchiveEntryNames.EntryType);
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

        private void Pack<T>(T entity, ZipArchive archive, string path)
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
                object obj;
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
                actionResult.AddError(String.Format(Resources.Message_Archive_entry_entity_type_is_empty_for_path_N, archiveEntry.Path));
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
            foreach (var entryPathElement in entryPathElements.Where(entryPathElement => !String.IsNullOrWhiteSpace(entryPathElement)))
            {
                if (builder.Length > 0)
                    builder.Append(@"\");
                builder.Append(entryPathElement);
            }
            return builder.ToString();
        }

        public IStage UnPackStage(string archiveFilePath, IActionResult actionResult)
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
                var sceneEntity = (ISceneEntity)entity;
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

        private static ZipArchiveEntry CreateEntry(ZipArchive archive, string entryPath)
        {
            return archive.CreateEntry(entryPath);
        }

        private static ZipArchiveEntry GetEntry(ZipArchive archive, string entryPath)
        {
            return archive.GetEntry(entryPath);
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
        private readonly Dictionary<Type, XmlSerializer> _xmlSerializers = new Dictionary<Type, XmlSerializer>();

        protected static void CreateArchiveIfDoesNotExist(string archiveFilePath)
        {
            if (File.Exists(archiveFilePath))
                return;
            using (var fileStream = new FileStream(archiveFilePath, FileMode.Create))
            using (new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                //create an empty archive
            }
        }

        protected XmlSerializer GetSerializerBy<TObject>(TObject obj)
        {
            return GetSerializerByType(obj.GetType());
        }

        protected XmlSerializer GetSerializer<TObject>()
        {
            return GetSerializerByType(typeof(TObject));
        }

        protected XmlSerializer GetSerializerByType(Type type)
        {
            if (!_xmlSerializers.ContainsKey(type))
                _xmlSerializers.Add(type, new XmlSerializer(type));
            return _xmlSerializers[type];
        }
    }
}