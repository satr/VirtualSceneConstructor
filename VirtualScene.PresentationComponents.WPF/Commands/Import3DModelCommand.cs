using Microsoft.Win32;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command importing 3D model
    /// </summary>
    internal class Import3DModelCommand : AddSceneObjectCommandBase
    {
        const string OpenFileDialogFilter = "Wavefont Obj Files (*.obj)|*.obj";
        /// <summary>
        /// Creates a new instance of the Import3DModel command
        /// </summary>
        /// <param name="sceneContent"></param>
        public Import3DModelCommand(SceneContent sceneContent)
            : base(sceneContent)
        {
        }

        protected override void Execute()
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = OpenFileDialogFilter,
                Title = Properties.Resources.Title_Import3D_model
            };
            var dialogResult = fileDialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
                return;
            ServiceLocator.Get<BusinessManager>().Import3DModel(fileDialog.FileName, SceneContent);
        }
    }
}