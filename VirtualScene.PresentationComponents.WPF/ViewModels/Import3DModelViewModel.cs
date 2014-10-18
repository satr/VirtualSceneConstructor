using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.Models;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view model of the import 3D model view
    /// </summary>
    public class Import3DModelViewModel
    {
        private Import3DModelModel _model;

        /// <summary>
        /// Creates a new instance of the import 3D view-model
        /// </summary>
        /// <param name="model">The model to keep state of the importing 3D model operation</param>
        public Import3DModelViewModel(Import3DModelModel model)
        {
            _model = model;
            OpenFileCommand = new OpenFileWith3DModelCommand(_model);
        }

        /// <summary>
        /// The command opening a dialog to select the file with 3D model
        /// </summary>
        public OpenFileWith3DModelCommand OpenFileCommand { get; set; }

        /// <summary>
        /// The file name of the imported 3D entity
        /// </summary>
        public string FileName
        {
            get { return _model.FullFileName; }
        }

        /// <summary>
        /// The name of the imported 3D entity
        /// </summary>
        public string Name
        {
            get { return _model.Name; }
        }
    }
}