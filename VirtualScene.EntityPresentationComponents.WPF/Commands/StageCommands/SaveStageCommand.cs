using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.EntityBusinessComponents;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.StageCommands
{
    /// <summary>
    /// The comman saving the stage
    /// </summary>
    public class SaveStageCommand : CommandWithSceneContentBase
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
        protected override void Execute()
        {
            var operationName = Resources.Title_Save_Stage;
            var entityNameDialogViewModel = new EntityNameDialogViewModel(operationName, SceneContent.Stage.Name);
            var entityNameDialogView = new EntityNameDialogView(entityNameDialogViewModel);
            while (true)
            {
                entityNameDialogView.ShowDialog();
                if (entityNameDialogViewModel.OperationCancelled)
                    return;
                SceneContent.Stage.Name = entityNameDialogViewModel.Name;
                var actionResult = BusinessManager.Save(SceneContent.Stage);
                if(actionResult.Success)
                    return;
                var operationFailedViewModel = DisplayOperationFailedDialog(operationName, actionResult);
                if(operationFailedViewModel.OperationCancelled)
                    break;
            }
        }

        private static OperationFailedViewModel DisplayOperationFailedDialog(string operationName, IActionResult actionResult)
        {
            var operationFailedViewModel = new OperationFailedViewModel(operationName, actionResult);
            new OperationFailedView(operationFailedViewModel).ShowDialog();
            return operationFailedViewModel;
        }

        private static StageBusinessManager BusinessManager
        {
            get { return ServiceLocator.Get<StageBusinessManager>(); }
        }
    }
}