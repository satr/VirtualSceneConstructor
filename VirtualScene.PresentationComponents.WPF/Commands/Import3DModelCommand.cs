using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Models;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command importing 3D model
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
            var model = new Import3DModelModel();
            var view = new Import3DModelView(model);
            view.ShowDialog();
            if (!view.DialogResult.HasValue || !view.DialogResult.Value)
                return;
            ServiceLocator.Get<BusinessManager>().Import3DModel(model.Name, model.FullFileName, SceneContent);
        }
    }
}