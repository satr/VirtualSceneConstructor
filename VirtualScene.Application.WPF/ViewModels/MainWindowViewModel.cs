using System.Windows;
using VirtualScene.Application.WPF.Views;
using VirtualScene.ApplicationPresentationComponents.WPF.Presenters;
using VirtualScene.Common;
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
        private FrameworkElement _contentView3;
        private FrameworkElement _contentView4;
        private FrameworkElement _contentView5;

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
            mainWindowView.KeyDown += (s, e) => applicationPresenter.SceneContent.Navigator.KeyboardAction(e);
            ContentView1 = applicationPresenter.GetStageContentView();
            ContentView2 = applicationPresenter.GetDetailView();
            ContentView3 = applicationPresenter.Get3DViewport1();
            ContentView4 = applicationPresenter.Get3DViewport2();
            ContentView5 = applicationPresenter.Get3DViewport3();
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

        /// <summary>
        /// The view hosted in the main-view on left side
        /// </summary>
        public FrameworkElement ContentView3
        {
            get { return _contentView3; }
            set
            {
                if (Equals(value, _contentView3)) 
                    return;
                _contentView3 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The view hosted in the main-view on left side
        /// </summary>
        public FrameworkElement ContentView4
        {
            get { return _contentView4; }
            set
            {
                if (Equals(value, _contentView4)) 
                    return;
                _contentView4 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The view hosted in the main-view on left side
        /// </summary>
        public FrameworkElement ContentView5
        {
            get { return _contentView5; }
            set
            {
                if (Equals(value, _contentView5)) 
                    return;
                _contentView5 = value;
                OnPropertyChanged();
            }
        }
    }
}