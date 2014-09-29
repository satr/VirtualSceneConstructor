using System.Collections.Generic;
using System.Windows.Controls;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Models
{
    /// <summary>
    ///     Scene viewport view model
    /// </summary>
    public class SceneViewportViewModel
    {
        private readonly SceneViewportModel _sceneViewportModel;

        /// <summary>
        ///     Creates a new instance of the view-model of the main window of the application.
        /// </summary>
        /// <param name="sceneViewModel"></param>
        public SceneViewportViewModel(SceneViewModel sceneViewModel)
        {
            _sceneViewportModel = new SceneViewportModel(sceneViewModel, SceneContent.Instance);
        }

        /// <summary>
        ///     Context menu for viewports
        /// </summary>
        public IEnumerable<MenuItem> ViewportContextMenu
        {
            get { return _sceneViewportModel.ViewportContextMenu; }
        }
    }
}