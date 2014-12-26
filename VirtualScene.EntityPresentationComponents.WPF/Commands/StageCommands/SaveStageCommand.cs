using System.IO;
using System.Windows.Forms;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.StageCommands
{
    /// <summary>
    /// The comman saving the stage
    /// </summary>
    public class SaveStageCommand : StorageOperationsStageCommandBase
    {
        /// <summary>
        /// Creates an new instance of the command
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public SaveStageCommand(ISceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Open the save-stage operation dialog
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            var operationName = Resources.Title_Save_Stage;
            while (true)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Title = operationName,
                    Filter = StageFilesFilter,
                    RestoreDirectory = true,
                    InitialDirectory = FolderPath,
                    FileName = SceneContent.Stage.Name
                };
                var dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult != DialogResult.OK)
                    return;
                FolderPath = Path.GetDirectoryName(saveFileDialog.FileName);
                SceneContent.Stage.Name = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                var actionResult = BusinessManager.Save(SceneContent.Stage);
                if(actionResult.Success)
                    return;
                var operationFailedViewModel = DisplayOperationFailedDialog(operationName, actionResult);
                if(operationFailedViewModel.OperationCancelled)
                    break;
            }
        }
    }
}