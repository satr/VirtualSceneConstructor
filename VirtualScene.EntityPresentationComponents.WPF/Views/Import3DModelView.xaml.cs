using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for Import3DModelView.xaml
    /// </summary>
    public partial class Import3DModelView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Import3DModelView" />
        /// </summary>
        /// <param name="viewModel">The view-model of the view</param>
        public Import3DModelView(WindowViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseView += (s,e) => Close();
        }
    }
}
