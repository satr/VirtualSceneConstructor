using System.Windows;
using VirtualScene.Application.WPF.ViewModels;

namespace VirtualScene.Application.WPF.Views
{
    /// <summary>
    /// The main window of the application
    /// </summary>
    public partial class MainWindowView : IMainWindowView
    {
        /// <summary>
        /// Initializes a new instance of the main window of the <see cref="MainWindowView" />
        /// </summary>
        public MainWindowView()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }

        /// <summary>
        /// Adds a UI controll element to the main view
        /// </summary>
        /// <param name="uiElement">UI control element</param>
        public void AddTopElement(UIElement uiElement)
        {
            TopPanel.Children.Add(uiElement);
        }
    }
}
