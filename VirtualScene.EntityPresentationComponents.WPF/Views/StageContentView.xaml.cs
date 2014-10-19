using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for StageContentView.xaml
    /// </summary>
    public partial class StageContentView
    {
        /// <summary>
        /// Creates a new instance of the content view for the stage
        /// </summary>
        /// <param name="viewModel"></param>
        public StageContentView(StageContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
