using System;
using System.Windows.Input;
using VirtualScene.Application.WPF.ViewModels;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.Application.WPF.Views
{
    /// <summary>
    /// The main window of the application
    /// </summary>
    public partial class MainWindowView
    {
        /// <summary>
        /// Creates a new instance of the main window of the application
        /// </summary>
        public MainWindowView()
        {
            InitializeComponent();
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
                SceneContent.Instance.SceneEngine.Move(movingStep, 0f, 0f);
            if(keyEventArgs.Key == Key.Down || keyEventArgs.Key == Key.S)
                SceneContent.Instance.SceneEngine.Move(-movingStep, 0f, 0f);
            if(keyEventArgs.Key == Key.Left || keyEventArgs.Key == Key.A)
                SceneContent.Instance.SceneEngine.Move(0f, movingStep, 0f);
            if(keyEventArgs.Key == Key.Right || keyEventArgs.Key == Key.D)
                SceneContent.Instance.SceneEngine.Move(0f, -movingStep, 0f);
            if(keyEventArgs.Key == Key.R || keyEventArgs.Key == Key.PageUp)
                SceneContent.Instance.SceneEngine.Move(0f, 0f, movingStep);
            if(keyEventArgs.Key == Key.F || keyEventArgs.Key == Key.PageDown)
                SceneContent.Instance.SceneEngine.Move(0f, 0f, -movingStep);
        }
    }
}
