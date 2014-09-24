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
            /// Default update rate for external 
            /// </summary>
            public const int DefaultUpdateRate = 40;//in milliseconds
        }
    }
}
