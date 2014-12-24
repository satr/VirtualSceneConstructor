using System;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.Entities;
using VirtualScene.EntityBusinessComponents;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.StageCommands
{
    /// <summary>
    /// The basic logic for commands operating with storage of stage
    /// </summary>
    public abstract class StorageOperationsStageCommandBase : CommandWithSceneContentBase
    {
        private readonly IFileSystemDataAdapter<IStage> _stageDataAdapter;

        /// <summary>
        /// Creates an new instance of the command
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        protected StorageOperationsStageCommandBase(ISceneContent sceneContent): base(sceneContent)
        {
            _stageDataAdapter = CreateDataAdapter();
            BusinessManager = new StageBusinessManager(_stageDataAdapter);
            StageFilesFilter = string.Format("{0} (*{1})|*{1}|{2} (*.*)|*.*", Resources.Title_Stage_files, 
                                            Constants.Stage.ArchiveFileExtension, Resources.Title_All_files);
        }

        private static StageFileSystemDataAdapter CreateDataAdapter()
        {
            return new StageFileSystemDataAdapter
            {
                EntityFolderPath = ServiceLocator.Get<FileSystemEnvironmentWrapper>().GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
        }

        /// <summary>
        /// The business-manager for operating with stages
        /// </summary>
        protected static StageBusinessManager BusinessManager { get; private set; }

        /// <summary>
        /// The stage files filter for save/open file dialogues.
        /// </summary>
        protected static string StageFilesFilter { get; private set; }

        /// <summary>
        /// Display the dialog informing about faulure during the operation and confirmation to repeat or cancel the operation.
        /// </summary>
        /// <param name="operationName">The name of the operation.</param>
        /// <param name="actionResult">The result of the confirmation.</param>
        /// <returns></returns>
        protected static OperationFailedViewModel DisplayOperationFailedDialog(string operationName, IActionResult actionResult)
        {
            var operationFailedViewModel = new OperationFailedViewModel(operationName, actionResult);
            new OperationFailedView(operationFailedViewModel).ShowDialog();
            return operationFailedViewModel;
        }

        /// <summary>
        /// The path to the folder where the stage is located.
        /// </summary>
        /// <value>The path to the folder.</value>
        protected string FolderPath
        {
            set { _stageDataAdapter.EntityFolderPath = value; }
            get { return _stageDataAdapter.EntityFolderPath; }
        }
    }
}