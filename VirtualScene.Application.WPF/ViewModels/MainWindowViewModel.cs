using System.Collections.Generic;
using System.Windows;
using VirtualScene.PresentationComponents.WPF;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.Application.WPF.ViewModels
{
    /// <summary>
    /// The view-model of the main window of the application.
    /// </summary>
    public class MainWindowViewModel
    {
        private readonly Presenter _presenter;

        /// <summary>
        /// Creates a new instance of the view-model of the main window of the application.
        /// </summary>
        public MainWindowViewModel()
        {
            _presenter = new Presenter();
        }

        /// <summary>
        /// UI-elements for the top view area
        /// </summary>
        public IEnumerable<UIElement> TopElements
        {
            get { return _presenter.TopElements; }
        }
    }
}