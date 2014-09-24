using System.Collections.ObjectModel;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The viewport containig a scene and the navigation component.
    /// </summary>
    public class SceneViewport
    {
        /// <summary>
        /// Creates the new instance of the viewport.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="cameras"></param>
        public SceneViewport(Scene scene, ObservableCollection<Camera> cameras)
        {
            Cameras = cameras;
            Scene = scene;
            Navigation = new SceneNavigation(scene);
        }

        /// <summary>
        /// Cameras in the scene
        /// </summary>
        public ObservableCollection<Camera> Cameras { get; private set; }

        /// <summary>
        /// The scene of the viewport.
        /// </summary>
        public Scene Scene { get; private set; }

        /// <summary>
        /// The component responcible for the navigation within scene of the viewport.
        /// </summary>
        public SceneNavigation Navigation { get; private set; }
    }
}