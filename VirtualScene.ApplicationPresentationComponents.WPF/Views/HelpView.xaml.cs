using VirtualScene.ApplicationPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.ApplicationPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView
    {
        /// <summary>
        /// Creates a new instance of the HelpView
        /// </summary>
        public HelpView()
        {
            InitializeComponent();
            DataContext = new HelpViewModel(Browser, Close);
        }
    }
}
