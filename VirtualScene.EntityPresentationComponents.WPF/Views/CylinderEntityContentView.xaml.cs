using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for CylinderEntityContentView.xaml
    /// </summary>
    public partial class CylinderEntityContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CylinderEntityContentView" />
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public CylinderEntityContentView(SceneEntityContentViewModel<CylinderEntity> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
