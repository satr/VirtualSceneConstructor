using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SceneEntityDetailView.xaml
    /// </summary>
    public partial class SceneEntityDetailView
    {
        /// <summary>
        /// Creates a new instance of the detail view for selected scene-entity
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public SceneEntityDetailView(SceneEntityDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
