using System.IO;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive;
using VirtualScene.DataComponents.Common.Properties;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    /// <summary>
    /// The data-adapter to store a stage in the file-system.
    /// </summary>
    public class StageFileSystemDataAdapter :  FileSystemDataAdapter<IStage>
    {
        private readonly StageArchiveManager _stageArchiveManager;

        /// <summary>
        /// Creates a new instance of the StageFileSystemDataAdapter
        /// </summary>
        public StageFileSystemDataAdapter()
        {
            _stageArchiveManager = new StageArchiveManager();
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

            _stageArchiveManager.PackStage(entity, GetArchiveFilePathFor(entity.Name));

            return actionResult;
        }

        /// <summary>
        /// Load the stage by its name.
        /// </summary>
        /// <param name="name">The name of the stage.</param>
        /// <returns>The result of the operation with loaded stage in action-result property Value.</returns>
        public override ActionResult<IStage> Load(string name)
        {
            var actionResult = new ActionResult<IStage>(Resources.Title_Load_the_stage);
            var archiveFilePath = GetArchiveFilePathFor(name);
            if (!File.Exists(archiveFilePath))
            {
                actionResult.AddError(Resources.Message_File_with_the_stage_does_not_exists);
                return actionResult;
            }
            
            actionResult.Value = _stageArchiveManager.UnPackStage(archiveFilePath, actionResult);

            return actionResult;
        }

        private string GetArchiveFilePathFor(string stageName)
        {
            return Path.Combine(EntityFolderPath, stageName + Constants.Stage.ArchiveFileExtension);
        }
    }
}