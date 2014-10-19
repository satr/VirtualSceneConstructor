using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command opening the dialog for importing 3D model
    /// </summary>
    internal class Import3DModelCommand : AddSceneObjectCommandBase
    {
        /// <summary>
        /// Creates a new instance of the Import3DModel command
        /// </summary>
        /// <param name="sceneContent"></param>
        public Import3DModelCommand(ISceneContent sceneContent)
            : base(sceneContent)
        {
        }

        protected override void Execute()
        {
            new Import3DModelView(new Import3DModelViewModel(SceneContent)).ShowDialog();
        }
    }
}