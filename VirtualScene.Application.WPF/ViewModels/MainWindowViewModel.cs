using System.Windows.Input;
using VirtualScene.Application.WPF.Views;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.Application.WPF.ViewModels
{
    /// <summary>
    /// The view-model of the main window of the application.
    /// </summary>
    public class MainWindowViewModel
    {
        private readonly SceneEngine _sceneEngine;

        /// <summary>
        /// Creates a new instance of the MainWindowViewModel
        /// </summary>
        /// <param name="mainWindowView">The main window view.</param>
        public MainWindowViewModel(IMainWindowView mainWindowView)
        {
            var applicationPresenter = ServiceLocator.Get<ApplicationPresenter>();
            _sceneEngine = applicationPresenter.SceneContent.SceneEngine;
            foreach (var uiElement in applicationPresenter.TopElements)
            {
                mainWindowView.AddTopElement(uiElement);
            }
            mainWindowView.KeyDown += HandleKeyEvent;
        }

        /// <summary>
        /// Invoked when the keyboard's lkey has been got down
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="keyEventArgs">Keys state</param>
        public void HandleKeyEvent(object sender, KeyEventArgs keyEventArgs)
        {
            const float movingStep = 0.2f;
            if (keyEventArgs.Key == Key.Up || keyEventArgs.Key == Key.W)
                _sceneEngine.Move(movingStep, 0f, 0f);
            if (keyEventArgs.Key == Key.Down || keyEventArgs.Key == Key.S)
                _sceneEngine.Move(-movingStep, 0f, 0f);
            if (keyEventArgs.Key == Key.Left || keyEventArgs.Key == Key.A)
                _sceneEngine.Move(0f, movingStep, 0f);
            if (keyEventArgs.Key == Key.Right || keyEventArgs.Key == Key.D)
                _sceneEngine.Move(0f, -movingStep, 0f);
            if (keyEventArgs.Key == Key.R || keyEventArgs.Key == Key.PageUp)
                _sceneEngine.Move(0f, 0f, movingStep);
            if (keyEventArgs.Key == Key.F || keyEventArgs.Key == Key.PageDown)
                _sceneEngine.Move(0f, 0f, -movingStep);

        }
    }
}