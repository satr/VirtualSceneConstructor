using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SpurGearEntityContentView.xaml
    /// </summary>
    public partial class SpurGearEntityContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearEntityContentView" />
        /// </summary>
        /// <param name="viewModel">The view model</param>
        public SpurGearEntityContentView(SceneEntityContentViewModel<SpurGearEntity> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
