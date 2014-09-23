using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Factory creating cameras
    /// </summary>
    public class CameraFactory
    {
        /// <summary>
        /// Creates a new camera
        /// </summary>
        /// <typeparam name="T">Type of the camera.</typeparam>
        /// <param name="position">Position of the camera in the scene.</param>
        /// <returns>Returns a new camera.</returns>
        public static Camera Create<T>(Vertex position)
            where T : Camera, new()
        {
            return new T
            {
                Position = position
            };
        }
    }
}