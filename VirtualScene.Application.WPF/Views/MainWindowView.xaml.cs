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
            if(keyEventArgs.Key == Key.Up)
                SceneContent.Instance.SceneEngine.Move(-0.5f, 0f, 0f);
            if(keyEventArgs.Key == Key.Down)
                SceneContent.Instance.SceneEngine.Move(0.5f, 0f, 0f);
            if(keyEventArgs.Key == Key.Left)
                SceneContent.Instance.SceneEngine.Move(0f, -0.5f, 0f);
            if(keyEventArgs.Key == Key.Right)
                SceneContent.Instance.SceneEngine.Move(0f, 0.5f, 0f);
            if(keyEventArgs.Key == Key.A)
                SceneContent.Instance.SceneEngine.Move(0f, 0f, -0.5f);
            if(keyEventArgs.Key == Key.X)
                SceneContent.Instance.SceneEngine.Move(0f, 0f, 0.5f);
        }
    }
}
