using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for HollowCylinderEntityContentView.xaml
    /// </summary>
    public partial class HollowCylinderEntityContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HollowCylinderEntityContentView" />
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public HollowCylinderEntityContentView(SceneEntityContentViewModel<HollowCylinderEntity> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
