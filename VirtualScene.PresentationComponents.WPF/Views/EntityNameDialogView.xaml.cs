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
        /// <param name="viewModel">The view-model to enter a name on the view</param>
        public EntityNameDialogView(WindowViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseView += (s, e) => Close();
        }
    }
}
