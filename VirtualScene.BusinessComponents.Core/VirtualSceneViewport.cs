using SharpGL.SceneGraph;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The viewport containig a scene and the navigation component.
    /// </summary>
    public class VirtualSceneViewport
    {
        /// <summary>
        /// Creates the new instance of the viewport.
        /// </summary>
        /// <param name="scene"></param>
        public VirtualSceneViewport(Scene scene)
        {
            Scene = scene;
            Navigation = new VirtualSceneNavigation(scene);
        }

        /// <summary>
        /// The scene of the viewport.
        /// </summary>
        public Scene Scene { get; private set; }

        /// <summary>
        /// The component responcible for the navigation within scene of the viewport.
        /// </summary>
        public VirtualSceneNavigation Navigation { get; private set; }
    }
}