using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The component responcible for the navigation within the scene
    /// </summary>
    public class SceneNavigation
    {
        /// <summary>
        /// Navigate withing the scene while the mouse moves
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="camera"></param>
        public void MouseMove(int x, int y, Camera camera)
        {
            var navigationCamera = GetNavigationCamera(camera);
            if (navigationCamera != null)
                navigationCamera.ArcBall.MouseMove(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-up event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="camera"></param>
        public void MouseUp(int x, int y, Camera camera)
        {
            var navigationCamera = GetNavigationCamera(camera);
            if (navigationCamera != null)
                navigationCamera.ArcBall.MouseUp(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-down event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="camera"></param>
        public void MouseDown(int x, int y, Camera camera)
        {
            var navigationCamera = GetNavigationCamera(camera);
            if (navigationCamera != null)
                navigationCamera.ArcBall.MouseDown(x, y);
        }

        private ArcBallCamera GetNavigationCamera(Camera camera)
        {
            return camera as ArcBallCamera;
        }

        /// <summary>
        /// Move the camera forward
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        public void Move(Camera camera, float dx, float dy, float dz)
        {
            var navigationCamera = GetNavigationCamera(camera);
            if (navigationCamera != null)
            {
                navigationCamera.Position = new Vertex(camera.Position.X + dx, camera.Position.Y + dy, camera.Position.Z + dz);
            }
        }
    }
}