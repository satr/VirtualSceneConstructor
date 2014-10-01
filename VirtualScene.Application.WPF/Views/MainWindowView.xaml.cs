using System.Windows.Input;
using VirtualScene.Application.WPF.ViewModels;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.Application.WPF.Views
{
    /// <summary>
    /// The main window of the application
    /// </summary>
    public partial class MainWindowView
    {
        private readonly SceneEngine _sceneEngine;

        /// <summary>
        /// Creates a new instance of the main window of the application
        /// </summary>
        public MainWindowView()
        {
            InitializeComponent();
            _sceneEngine = ServiceLocator.Get<ApplicationPresenter>().SceneContent.SceneEngine;
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            foreach (var element in viewModel.TopElements)
            {
                TopPanel.Children.Add(element);
            }
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            const float movingStep = 0.2f;
            if(keyEventArgs.Key == Key.Up || keyEventArgs.Key == Key.W)
                _sceneEngine.Move(movingStep, 0f, 0f);
            if(keyEventArgs.Key == Key.Down || keyEventArgs.Key == Key.S)
                _sceneEngine.Move(-movingStep, 0f, 0f);
            if(keyEventArgs.Key == Key.Left || keyEventArgs.Key == Key.A)
                _sceneEngine.Move(0f, movingStep, 0f);
            if(keyEventArgs.Key == Key.Right || keyEventArgs.Key == Key.D)
                _sceneEngine.Move(0f, -movingStep, 0f);
            if(keyEventArgs.Key == Key.R || keyEventArgs.Key == Key.PageUp)
                _sceneEngine.Move(0f, 0f, movingStep);
            if(keyEventArgs.Key == Key.F || keyEventArgs.Key == Key.PageDown)
                _sceneEngine.Move(0f, 0f, -movingStep);
        }
    }
}
