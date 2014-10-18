using VirtualScene.Application.WPF.Views;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.Application.WPF.ViewModels
{
    /// <summary>
    /// The view-model of the main window of the application.
    /// </summary>
    public class MainWindowViewModel
    {
        /// <summary>
        /// Creates a new instance of the MainWindowViewModel
        /// </summary>
        /// <param name="mainWindowView">The main window view.</param>
        public MainWindowViewModel(IMainWindowView mainWindowView)
        {
            var applicationPresenter = ServiceLocator.Get<ApplicationPresenter>();
            foreach (var uiElement in applicationPresenter.TopElements)
            {
                mainWindowView.AddTopElement(uiElement);
            }
            mainWindowView.KeyDown += (s, e) => applicationPresenter.SceneContent.Navigation.KeyboardAction(e);
        }
    }
}