using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view model for EntityNameDialogView
    /// </summary>
    public class EntityNameDialogViewModel: ViewModelBase
    {
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNameDialogViewModel" />
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="name">The entity name</param>
        public EntityNameDialogViewModel(string title, string name)
        {
            Title = title;
            _name = name;
            AcceptCommand = new DelegateCommand(OnCloseView);
        }

        /// <summary>
        /// The command on the dialog accepted
        /// </summary>
        public DelegateCommand AcceptCommand { get; set; }

        /// <summary>
        /// The title of the dialog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) 
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}