using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SceneEntityContentView.xaml
    /// </summary>
    public partial class SceneEntityContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneEntityContentView" />
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public SceneEntityContentView(SceneEntityContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
