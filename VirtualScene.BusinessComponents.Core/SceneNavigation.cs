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
            var arcBallCamera = GetArcBallCamera(camera);
            if (arcBallCamera != null)
                arcBallCamera.ArcBall.MouseMove(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-up event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="camera"></param>
        public void MouseUp(int x, int y, Camera camera)
        {
            var arcBallCamera = GetArcBallCamera(camera);
            if (arcBallCamera != null)
                arcBallCamera.ArcBall.MouseUp(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-down event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="camera"></param>
        public void MouseDown(int x, int y, Camera camera)
        {
            var arcBallCamera = GetArcBallCamera(camera);
            if (arcBallCamera != null)
                arcBallCamera.ArcBall.MouseDown(x, y);
        }

        private static ArcBallCamera GetArcBallCamera(Camera camera)
        {
            return camera as ArcBallCamera;
        }

        private static LookAtCamera GetLookAtCamera(Camera camera)
        {
            return camera as LookAtCamera;
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
            if(GetArcBallCamera(camera) != null)
                return;
            var lookAtCamera = GetLookAtCamera(camera);
            if (lookAtCamera != null)
            {
                lookAtCamera.Position = OffsetLookAtCamera(dx, dy, dz, lookAtCamera.Position);
                lookAtCamera.Target = OffsetLookAtCamera(dx, dy, dz, lookAtCamera.Target);
            }
        }

        private static Vertex OffsetLookAtCamera(float dx, float dy, float dz, Vertex pos)
        {
            return new Vertex(pos.X + dx, pos.Y + dy, pos.Z + dz);
        }
    }
}