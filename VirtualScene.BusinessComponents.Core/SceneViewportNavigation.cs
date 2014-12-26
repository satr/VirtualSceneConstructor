using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The component responsible for the navigation within the scene
    /// </summary>
    public class SceneViewportNavigation
    {
        private readonly ISceneViewport _sceneViewport;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneViewportNavigation" />
        /// </summary>
        /// <param name="sceneViewport"></param>
        public SceneViewportNavigation(ISceneViewport sceneViewport)
        {
            _sceneViewport = sceneViewport;
        }

        /// <summary>
        /// Navigate withing the scene while the mouse moves
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void MouseMove(int x, int y)
        {
            var arcBallCamera = GetArcBallCamera();
            if (arcBallCamera != null)
                arcBallCamera.ArcBall.MouseMove(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-up event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void MouseUp(int x, int y)
        {
            var arcBallCamera = GetArcBallCamera();
            if (arcBallCamera != null)
                arcBallCamera.ArcBall.MouseUp(x, y);

        }

        /// <summary>
        /// Notify the current navigation camera abour the mouse-down event
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void MouseDown(int x, int y)
        {
            var arcBallCamera = GetArcBallCamera();
            if (arcBallCamera != null)
                arcBallCamera.ArcBall.MouseDown(x, y);
        }

        /// <summary>
        /// Move the camera forward
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        public void Move(float dx, float dy, float dz)
        {
            if(GetArcBallCamera() != null)
                return;
            var lookAtCamera = GetLookAtCamera();
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

        private ArcBallCamera GetArcBallCamera()
        {
            return _sceneViewport.CurrentCamera as ArcBallCamera;
        }

        private LookAtCamera GetLookAtCamera()
        {
            return _sceneViewport.CurrentCamera as LookAtCamera;
        }
    }
}