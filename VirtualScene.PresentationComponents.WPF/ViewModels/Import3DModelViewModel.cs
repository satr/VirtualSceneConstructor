using System;
using System.Windows.Input;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view model of the import 3D model view
    /// </summary>
    public class Import3DModelViewModel: ViewModelBase
    {
        /// <summary>
        /// Occures when the view should be closed.
        /// </summary>
        public event EventHandler CloseView;

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
            CancelCommand = new DelegateCommand(OnCloseView);
        }

        /// <summary>
        /// The command closing the view
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

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

        /// <summary>
        /// The operation status. When  "true" - the operation has been cancelled
        /// </summary>
        public bool OperationCancelled { get; set; }

        private  void OnCloseView()
        {
            var handler = CloseView;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }
    }
}