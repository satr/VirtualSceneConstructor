using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for OperationFailedView.xaml
    /// </summary>
    public partial class OperationFailedView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationFailedView" />
        /// </summary>
        /// <param name="viewModel">The view-model of the view</param>
        public OperationFailedView(OperationFailedViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseView += (s, e) => Close();
        }
    }
}
