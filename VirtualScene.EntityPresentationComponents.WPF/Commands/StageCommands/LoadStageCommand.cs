using System.IO;
using System.Windows.Forms;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.StageCommands
{
    /// <summary>
    /// The comman loading the stage
    /// </summary>
    public class LoadStageCommand : StorageOperationsStageCommandBase
    {
        /// <summary>
        /// Creates an new instance of the command
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public LoadStageCommand(ISceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Open the load-stage operation dialog
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            var operationName = Resources.Title_Load_stage;
            var openFileDialog = new OpenFileDialog()
                {
                    Title = operationName,
                    Filter = StageFilesFilter,
                    RestoreDirectory = true,
                    InitialDirectory = FolderPath,
                    Multiselect = false
                };
            while (true)
            {
                var dialogResult = openFileDialog.ShowDialog();
                if (dialogResult != DialogResult.OK)
                    return;
                var fileName = openFileDialog.FileName;
                FolderPath = Path.GetDirectoryName(fileName);
                var actionResult = BusinessManager.Load(Path.GetFileNameWithoutExtension(fileName));
                if (actionResult.Success)
                {
                    SceneContent.Stage = actionResult.Value;
                    return;
                }
                var operationFailedViewModel = DisplayOperationFailedDialog(operationName, actionResult);
                if(operationFailedViewModel.OperationCancelled)
                    return;
            }
        }
    }
}