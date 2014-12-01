using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    internal class StageFileSystemDataAdapter : FileSystemDataAdapter<IStage>
    {
        private readonly Dictionary<Type, XmlSerializer> _xmlSerializers = new Dictionary<Type, XmlSerializer>();

        private static class ArchiveEntryNames
        {
            public const string SceneEntities = "SceneEntities";
        }

        public override IActionResult Save(IStage entity)
        {
            var actionResult = new ActionResult<IStage>(Resources.Title_Save_Save_the_stage);
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                actionResult.AddError(Resources.Message_Save_Name_of_the_stage_is_invalid);
                return actionResult;
            }
            
            CreateFolderIfDoesNotExist(StagesFolderPath);
            
            var archiveFilePath = GetArchiveFilePathFor(entity);
            CreateArchiveIfDoesNotExist(archiveFilePath);

            Archive(entity, archiveFilePath);
            
            return actionResult;
        }

        /// <summary>
        /// Get the archive file full path for the stage.
        /// </summary>
        /// <param name="entity">The stage.</param>
        /// <returns>THe path to the archive file.</returns>
        public string GetArchiveFilePathFor(IStage entity)
        {
            return Path.Combine(StagesFolderPath, entity.Name + ".zip");
        }

        private void Archive(IStage entity, string archiveFilePath)
        {
            using (var fileStream = new FileStream(archiveFilePath, FileMode.Open))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Update))
            {
                foreach (var sceneEntity in entity.Entities)
                {
                    var sceneEntitiesArchive = GetOrCreateArchiveEntity(archive, GetSceneEntityArchiveEntryNameBy(sceneEntity));
                    using (var archiveStream = new StreamWriter(sceneEntitiesArchive.Open()))
                    {
                        GetSerializerBy(sceneEntity.Geometry).Serialize(archiveStream, sceneEntity.Geometry);
                    }
                }
            }
        }

        private static string GetSceneEntityArchiveEntryNameBy(ISceneEntity sceneEntity)
        {
            return string.Format(@"{0}\{1}", ArchiveEntryNames.SceneEntities, sceneEntity.Name);
        }

        private XmlSerializer GetSerializerBy(SceneElement sceneElement)
        {
            var type = sceneElement.GetType();
            if (!_xmlSerializers.ContainsKey(type))
                _xmlSerializers.Add(type, new XmlSerializer(type));
            return _xmlSerializers[type];
        }

        private static void CreateArchiveIfDoesNotExist(string archiveFilePath)
        {
            using (var fileStream = new FileStream(archiveFilePath, FileMode.Create))
            using (new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                //create an empty archive
            }
        }

        private static ZipArchiveEntry GetOrCreateArchiveEntity(ZipArchive archive, string entryName)
        {
            return archive.GetEntry(entryName) ?? archive.CreateEntry(entryName, CompressionLevel.NoCompression);
        }
    }
}