using System.Linq;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
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
            var stageName = SceneContent.Stage.Name;
            var operationName = Resources.Title_Save_Stage;
            var viewModel = new EntityNameDialogViewModel(operationName, stageName);
            do
            {
                new EntityNameDialogView(viewModel).ShowDialog();
                if (viewModel.OperationCancelled)
                    return;
                var actionResult = ServiceLocator.Get<StageBusinessManager>().Save(SceneContent.Stage, viewModel.Name);
                if(actionResult.Success)
                    return;
                var operationFailedViewModel = new OperationFailedViewModel(operationName, actionResult.Errors.ToArray());
                new OperationFailedView(operationFailedViewModel).ShowDialog();
                if(operationFailedViewModel.OperationCancelled)
                    break;
            } while (true);
        }
    }
}