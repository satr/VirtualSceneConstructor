using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The component responcible for the navigation within the scene
    /// </summary>
    public class VirtualSceneNavigation
    {
        private readonly Scene _scene;

        /// <summary>
        /// Creates a new instance The component responcible for the navigation within the scene
        /// </summary>
        /// <param name="scene">The scene</param>
        public VirtualSceneNavigation(Scene scene)
        {
            _scene = scene;
        }

        /// <summary>
        /// Navigate withing the scene while the mouse moves
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void MouseMove(int x, int y)
        {
            var camera = NavigationCamera;
            if (camera != null)
                camera.ArcBall.MouseMove(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-up event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void MouseUp(int x, int y)
        {
            var camera = NavigationCamera;
            if (camera != null)
                camera.ArcBall.MouseUp(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-down event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void MouseDown(int x, int y)
        {
            var camera = NavigationCamera;
            if (camera != null)
                camera.ArcBall.MouseDown(x, y);
        }

        private ArcBallCamera NavigationCamera
        {
            get { return _scene.CurrentCamera as ArcBallCamera; }
        }
    }
}