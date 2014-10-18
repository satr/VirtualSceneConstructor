using Microsoft.Win32;
using VirtualScene.PresentationComponents.WPF.Models;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command opening the dialog to choose the file with 3D model
    /// </summary>
    public class OpenFileWith3DModelCommand: CommandBase
    {
        private readonly Import3DModelModel _model;

        /// <summary>
        /// Creates a new instance of the command
        /// </summary>
        /// <param name="model">The model to keep state of the importing 3D model operation</param>
        public OpenFileWith3DModelCommand(Import3DModelModel model)
        {
            _model = model;
        }

        const string OpenFileDialogFilter = "Wavefont Obj Files (*.obj)|*.obj";

        /// <summary>
        /// Executes the command to open the dialog to select a file with 3D model
        /// </summary>
        protected override void Execute()
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = OpenFileDialogFilter,
                Title = Properties.Resources.Title_Import3D_model
            };
            _model.OperationCancelled = fileDialog.ShowDialog()?? false;
            if(_model.OperationCancelled)
                return;
            _model.FullFileName = fileDialog.FileName;
        }
    }
}