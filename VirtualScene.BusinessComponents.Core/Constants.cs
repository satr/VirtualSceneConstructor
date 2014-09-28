using SharpGL.SceneGraph;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Constants of the Virtual scene
        /// </summary>
        public static class Scene
        {
            /// <summary>
            /// Minimum width of the scene
            /// </summary>
            public const int MinimumWidth = 800;

            /// <summary>
            /// Minimum height of the scene
            /// </summary>
            public const int MinimumHeight = 600;

            /// <summary>
            /// Minimum color depth of the scene
            /// </summary>
            public const int MinimumColorDepth = 8;

            /// <summary>
            /// Maxinim color depth of the scene
            /// </summary>
            public const int MaximumColorDepth = 32;

            /// <summary>
            /// By default the scene engine contains a design primitives folder and folder with grid and a axises.
            /// </summary>
            public const int DefaultSceneElementsCount = 2;
        }


        /// <summary>
        /// Constants for SceneEngine
        /// </summary>
        public class SceneEngine
        {
            /// <summary>
            /// Default position and orientation of the camera
            /// </summary>
            public static Vertex DefaultCameraVector = new Vertex(-10, -10, 10);

            /// <summary>
            /// Default update rate, in milliseconds
            /// </summary>
            public const int DefaultUpdateRate = 40;
        }
    }
}
