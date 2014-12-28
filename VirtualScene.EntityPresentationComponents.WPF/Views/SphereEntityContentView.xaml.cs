using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SphereEntityContentView.xaml
    /// </summary>
    public partial class SphereEntityContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SphereEntityContentView" />
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public SphereEntityContentView(SceneEntityContentViewModel<SphereEntity> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
