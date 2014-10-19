using System.Windows;
using VirtualScene.Application.WPF.Views;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.PresentationComponents.WPF.Presenters;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.Application.WPF.ViewModels
{
    /// <summary>
    /// The view-model of the main window of the application.
    /// </summary>
    public class MainWindowViewModel: ViewModelBase
    {
        private FrameworkElement _contentView1;
        private FrameworkElement _contentView2;

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
            ContentView1 = applicationPresenter.GetStageContentView();
            ContentView2 = applicationPresenter.GetDetailView();
        }

        /// <summary>
        /// The view hosted in the main-view on left side
        /// </summary>
        public FrameworkElement ContentView1
        {
            get { return _contentView1; }
            set
            {
                if (Equals(value, _contentView1)) 
                    return;
                _contentView1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The view hosted in the main-view on left side
        /// </summary>
        public FrameworkElement ContentView2
        {
            get { return _contentView2; }
            set
            {
                if (Equals(value, _contentView2)) 
                    return;
                _contentView2 = value;
                OnPropertyChanged();
            }
        }
    }
}