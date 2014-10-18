using VirtualScene.PresentationComponents.WPF.Models;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for Import3DModelView.xaml
    /// </summary>
    public partial class Import3DModelView
    {
        /// <summary>
        /// Creates a new instance of the view accepting parameters for importing of 3D model
        /// </summary>
        /// <param name="model">The model of the view</param>
        public Import3DModelView(Import3DModelModel model)
        {
            InitializeComponent();
            DataContext = new Import3DModelViewModel(model);
        }
    }
}
