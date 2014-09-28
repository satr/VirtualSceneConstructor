using System.Collections.Generic;
using System.Windows.Controls;
using SharpGL.WPF;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF
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
        /// <param name="sceneView"></param>
        public SceneViewportViewModel(SceneView sceneView)
        {

            _sceneViewportModel = new SceneViewportModel(sceneView, SceneContent.Instance);

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