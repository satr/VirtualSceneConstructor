using VirtualScene.ApplicationPresentationComponents.WPF.ViewModels;

namespace VirtualScene.ApplicationPresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpView" />
        /// </summary>
        public HelpView()
        {
            InitializeComponent();
            DataContext = new HelpViewModel(Browser, Close);
        }
    }
}
