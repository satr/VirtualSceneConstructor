using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using SharpGL.SceneGraph.Core;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Properties;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    /// <summary>
    /// Procvessing the archive with a stage.
    /// </summary>
    internal class StageArchiveManager
    {
        private readonly EntityPackerManager _entityPackerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StageArchiveManager"/>
        /// </summary>
        public StageArchiveManager()
        {
            _entityPackerManager = new EntityPackerManager();
        }

        /// <summary>
        /// Pack the stage to the archive.
        /// </summary>
        /// <param name="stage">The stage to pack.</param>
        /// <param name="archiveFilePath">The path to the archive where the stage is packed.</param>
        public void PackStage(IStage stage, string archiveFilePath)
        {
            if (stage == null)
                throw new ArgumentNullException(Resources.Message_Archive_Entity_cannot_be_saved_because_it_is_empty);

            CreateDirectoryForFileIfNotExists(archiveFilePath);

            using (var fileStream = File.Create(archiveFilePath))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                _entityPackerManager.Pack(stage, archive, string.Empty);
                foreach (var sceneEntity in stage.Items)
                {
                    var sceneEntityArchiveName = Guid.NewGuid().ToString();
                    _entityPackerManager.Pack(sceneEntity, archive, ArchiveEntryNames.Items, sceneEntityArchiveName);
                    _entityPackerManager.Pack(sceneEntity.Geometry, archive, ArchiveEntryNames.Items, sceneEntityArchiveName);
                }
            }
        }

        /// <summary>
        /// Unpach the stage from the archive.
        /// </summary>
        /// <param name="archiveFilePath">The path to the archive with the stage.</param>
        /// <param name="actionResult">The result of the unpacking of the archive.</param>
        /// <returns>The unpacked stage.</returns>
        public IStage UnPackStage(string archiveFilePath, IActionResult actionResult)
        {
            try
            {
                CreateDirectoryForFileIfNotExists(archiveFilePath);
                using (var fileStream = File.OpenRead(archiveFilePath))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                {
                    var hierarchyResult = GetArchiveHierarchy(archive.Entries);
                    if (!hierarchyResult.Success)
                    {
                        actionResult.CombineWith(hierarchyResult);
                        return null;
                    }
                    var stageArchiveEntry = hierarchyResult.Value;
                    var stageEntity = UnPackEntity(stageArchiveEntry, actionResult);
                    ValidateEntity<IStage>(stageEntity, actionResult);
                    if (!actionResult.Success)
                        return null;
                    var stage = (IStage)stageEntity;
                    if (stageArchiveEntry.Items != null)
                        UnPackSceneEntities(stage, stageArchiveEntry.Items, actionResult);
                    return stage;
                }
            }
            catch (InvalidDataException e)
            {
                actionResult.AddError(Resources.Message_The_archive_N_with_a_stage_might_be_currupted_M, archiveFilePath, e.Message);
            }
            catch (Exception e)
            {
                actionResult.AddError(Resources.Message_The_archive_N_with_a_stage_cannot_be_processed_due_to_the_error_M, archiveFilePath, e.Message);
            }
            return null;
        }

        private static void CreateDirectoryForFileIfNotExists(string archiveFilePath)
        {
            var archiveFileInfo = new FileInfo(archiveFilePath);
            if (archiveFileInfo.Directory != null && !archiveFileInfo.Directory.Exists)
                archiveFileInfo.Directory.Create();
        }

        private object UnPackEntity(IArchiveEntry<ZipArchiveEntry> archiveEntry, IActionResult actionResult)
        {
            var entryType = UnPackEntityType(archiveEntry, actionResult);
            return UnPackEntity(archiveEntry, entryType, actionResult);
        }

        private Type UnPackEntityType(IArchiveEntry<ZipArchiveEntry> archiveEntry, IActionResult actionResult)
        {
            if (archiveEntry.EntityType != null) 
                return UnPackEntity(archiveEntry.EntityType, typeof (TypeInfo), actionResult) as Type;

            actionResult.AddError(String.Format(Resources.Message_Archive_entry_entity_type_is_empty_for_path_N, archiveEntry.Path));
            return null;
        }

        private object UnPackEntity(IArchiveEntry<ZipArchiveEntry> archiveEntry, Type entryType, IActionResult actionResult)
        {
            if (archiveEntry.Entity != null) 
                return UnPackEntity(archiveEntry.Entity, entryType, actionResult);
            
            actionResult.AddError(Resources.Message_Archive_entry_entity_is_empty_for_path_N, archiveEntry.Path);
            return null;
        }

        private object UnPackEntity(ZipArchiveEntry archiveEntry, Type type, IActionResult actionResult)
        {
            try
            {
                using (var stream = archiveEntry.Open())
                {
                    return _entityPackerManager.UnPack(type, stream);
                }
            }
            catch (Exception e)
            {
                actionResult.AddError(Resources.Message_Archive_entry_object_cannot_be_deserialized_N, e.Message);
            }
            return null;
        }


        private void UnPackSceneEntities(IStage stage, IEnumerable<IArchiveEntry<ZipArchiveEntry>> sceneEntityArchiveEntries, IActionResult actionResult)
        {
            foreach (var sceneEntityArchiveEntry in sceneEntityArchiveEntries)
            {
                var sceneEntityActionResult = new ActionResult<ISceneEntity>();
                var entity = UnPackEntity(sceneEntityArchiveEntry, sceneEntityActionResult);
                
                ValidateEntity<ISceneEntity>(entity, sceneEntityActionResult);

                if (sceneEntityActionResult.Success)
                {
                    var sceneEntity = (ISceneEntity) entity;

                    if (sceneEntityArchiveEntry.Geometry != null)
                        UnPackGeometry(sceneEntityArchiveEntry.Geometry, sceneEntity, sceneEntityActionResult);

                    if (sceneEntityActionResult.Success)
                        stage.Items.Add(sceneEntity);
                }
                actionResult.CombineWith(sceneEntityActionResult);
            }
        }

        private void UnPackGeometry(IArchiveEntry<ZipArchiveEntry> geometryArchiveEntry, ISceneEntity sceneEntity, IActionResult actionResult)
        {
            var geometryEntity = UnPackEntity(geometryArchiveEntry, actionResult);
            ValidateEntity<SceneElement>(geometryEntity, actionResult);
            if (!actionResult.Success) 
                return;

            sceneEntity.Geometry = (SceneElement) geometryEntity;

            if (geometryArchiveEntry.Transformation != null)
                UnpackGeometryTransformation(geometryArchiveEntry, sceneEntity, actionResult);
        }

        private void UnpackGeometryTransformation(IArchiveEntry<ZipArchiveEntry> geometryArchiveEntry, ISceneEntity sceneEntity, IActionResult actionResult)
        {
            var objectSpace = sceneEntity.Geometry as IHasObjectSpace;
            if (objectSpace == null)
            {
                actionResult.AddError(Resources.Message_Transformation_is_found_by_the_entity_does_not_need_it_N, geometryArchiveEntry.Path);
                return;
            }
            var transformationEntity = UnPackEntity(geometryArchiveEntry.Transformation, actionResult);
            ValidateEntity<Transformation>(transformationEntity, actionResult);
            if (!actionResult.Success)
                return;
            ((Transformation) transformationEntity).Transform(objectSpace.Transformation);
        }

        private static void ValidateEntity<T>(object entity, IActionResult actionResult)
        {
            if (entity == null || !actionResult.Success)
                return;
            if (!(entity is T))
                actionResult.AddError(Resources.Message_A_type_N_was_expected_by_the_following_type_was_loaded_M, typeof(T), entity.GetType());
        }

        private static ActionResult<IArchiveEntry<ZipArchiveEntry>> GetArchiveHierarchy(IEnumerable<ZipArchiveEntry> entries)
        {
            var builder = new ArchiveHierarchyBuilder<ZipArchiveEntry>();
            foreach (var archiveEntry in entries)
            {
                builder.Add(archiveEntry, archiveEntry.FullName);
            }
            return builder.GetValidatedHierarchy();
        }

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
    }
}