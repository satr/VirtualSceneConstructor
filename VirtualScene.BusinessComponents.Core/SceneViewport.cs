using SharpGL.SceneGraph;

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
        public SceneViewport(Scene scene)
        {
            Scene = scene;
            Navigation = new SceneNavigation(scene);
        }

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