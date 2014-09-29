using System.Collections.Generic;
using System.Windows;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.Application.WPF.ViewModels
{
    /// <summary>
    /// The view-model of the main window of the application.
    /// </summary>
    public class MainWindowViewModel
    {
        private readonly ApplicationPresenter _applicationPresenter;

        /// <summary>
        /// Creates a new instance of the view-model of the main window of the application.
        /// </summary>
        public MainWindowViewModel()
        {
            _applicationPresenter = new ApplicationPresenter();
        }

        /// <summary>
        /// UI-elements for the top view area
        /// </summary>
        public IList<UIElement> TopElements
        {
            get { return _applicationPresenter.TopElements; }
        }
    }
}