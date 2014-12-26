using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command opening the dialog for importing 3D model
    /// </summary>
    public class Import3DModelCommand : AddSceneObjectCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Import3DModelCommand" />
        /// </summary>
        /// <param name="sceneContent"></param>
        public Import3DModelCommand(ISceneContent sceneContent)
            : base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            new Import3DModelView(new Import3DModelViewModel(SceneContent)).ShowDialog();
        }
    }
}