using VirtualScene.PresentationComponents.WPF.Models;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for EntityNameDialogView.xaml
    /// </summary>
    public partial class EntityNameDialogView
    {
        /// <summary>
        /// Creates a new oinstance of the EntityNameDialogView
        /// </summary>
        /// <param name="title"></param>
        /// <param name="entityNameViewModel"></param>
        public EntityNameDialogView(string title, EntityNameViewModel entityNameViewModel)
        {
            InitializeComponent();
            DataContext = new EntityNameDialogViewModel(title, this, entityNameViewModel);
        }
    }
}
