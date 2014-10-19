using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SceneEntityContentView.xaml
    /// </summary>
    public partial class SceneEntityContentView
    {
        /// <summary>
        /// Creates a new instance of the detail view for selected scene-entity
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public SceneEntityContentView(SceneEntityContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
