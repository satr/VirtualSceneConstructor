using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Views;
using Import3DModelView = VirtualScene.EntityPresentationComponents.WPF.Views.Import3DModelView;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command opening the dialog for importing 3D model
    /// </summary>
    public class Import3DModelCommand : AddSceneObjectCommandBase
    {
        /// <summary>
        /// Creates a new instance of the Import3DModel command
        /// </summary>
        /// <param name="sceneContent"></param>
        public Import3DModelCommand(ISceneContent sceneContent)
            : base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected override void Execute()
        {
            new Import3DModelView(new Import3DModelViewModel(SceneContent)).ShowDialog();
        }
    }
}