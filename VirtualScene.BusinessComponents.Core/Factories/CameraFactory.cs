using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core.Factories
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
        /// <param name="name"></param>
        /// <returns>Returns a new camera.</returns>
        public static Camera Create<T>(Vertex position, string name = null)
            where T : Camera, new()
        {
            var camera = new T
            {
                Position = position
            };
            if (!string.IsNullOrWhiteSpace(name))
                camera.Name = name;
            return camera;
        }
    }
}