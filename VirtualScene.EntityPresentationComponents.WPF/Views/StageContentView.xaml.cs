using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for StageContentView.xaml
    /// </summary>
    public partial class StageContentView
    {
        /// <summary>
        /// Initializes a new instance of the content <see cref="StageContentView" />
        /// </summary>
        /// <param name="viewModel"></param>
        public StageContentView(StageContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
