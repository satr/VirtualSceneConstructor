using System.Windows.Input;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.EntityPresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view model of the import 3D model view
    /// </summary>
    public class Import3DModelViewModel: ViewModelBase
    {
        private string _fileName;

        private string _name;

        /// <summary>
        /// Creates a new instance of the import 3D view-model
        /// </summary>
        /// <param name="sceneContent"></param>
        public Import3DModelViewModel(ISceneContent sceneContent)
        {
            OpenFileCommand = new OpenFileWith3DModelCommand(this);
            ImportCommand = new PerformImport3DModelCommand(this, sceneContent).AfterExecuteAction(OnCloseView);
        }

        /// <summary>
        /// The command performing the import from the file
        /// </summary>
        public ICommand ImportCommand { get; set; }

        /// <summary>
        /// The command opening a dialog to select the file with 3D model
        /// </summary>
        public OpenFileWith3DModelCommand OpenFileCommand { get; set; }

        /// <summary>
        /// The file name of the imported 3D entity
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (Equals(_fileName = value))
                    return;
                _fileName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The name of the imported 3D entity
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if(Equals(_name = value))
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}