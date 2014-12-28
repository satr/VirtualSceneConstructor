using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for CubeEntityContentView.xaml
    /// </summary>
    public partial class CubeEntityContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubeEntityContentView" />
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public CubeEntityContentView(SceneEntityContentViewModel<CubeEntity> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
