using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command importing 3D model
    /// </summary>
    public class PerformImport3DModelCommand : CommandBase
    {
        private readonly Import3DModelViewModel _viewModel;
        private readonly ISceneContent _sceneContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformImport3DModelCommand" />
        /// </summary>
        /// <param name="viewModel">Th view-model of the importing view</param>
        /// <param name="sceneContent">The content of the scene</param>
        public PerformImport3DModelCommand(Import3DModelViewModel viewModel, ISceneContent sceneContent)
        {
            _viewModel = viewModel;
            _sceneContent = sceneContent;
        }

        /// <summary>
        /// Perform the import of 3D model from the selected file
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            ServiceLocator.Get<SceneContentBusinessManager>().Import3DModel(_viewModel.Name, _viewModel.FileName, _sceneContent);
        }
    }
}