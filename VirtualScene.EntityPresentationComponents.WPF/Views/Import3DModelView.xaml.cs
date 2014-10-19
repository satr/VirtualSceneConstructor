using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for Import3DModelView.xaml
    /// </summary>
    public partial class Import3DModelView
    {
        /// <summary>
        /// Creates a new instance of the view accepting parameters for importing of 3D model
        /// </summary>
        /// <param name="viewModel">The view-model of the view</param>
        public Import3DModelView(Import3DModelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseView += (s,e) => Close();
        }
    }
}
