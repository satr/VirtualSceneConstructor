using System.Collections.Generic;
using System.Windows;
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
        /// UI-elements for the top view area
        /// </summary>
        public IList<UIElement> TopElements
        {
            get { return ServiceLocator.Get<ApplicationPresenter>().TopElements; }
        }
    }
}