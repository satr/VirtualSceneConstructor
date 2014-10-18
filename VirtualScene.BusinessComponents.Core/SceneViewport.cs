using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The viewport containig a scene and the navigation component.
    /// </summary>
    public class SceneViewport: ISceneViewport
    {
        private const int MinSceneWidth = 20;
        private const int MinSceneHeight = 20;
        private int _lastSizeChangedWidth;
        private int _lastSizeChangedHeight;

        /// <summary>
        /// Creates the new instance of the viewport.
        /// </summary>
        /// <param name="scene"></param>
        public SceneViewport(Scene scene)
        {
            Scene = scene;
            CurrentCamera = Scene.CurrentCamera;
            Navigation = new SceneViewportNavigation(this);
        }

        /// <summary>
        /// The scene of the viewport.
        /// </summary>
        public Scene Scene { get; private set; }

        /// <summary>
        /// The component responcible for the navigation within scene of the viewport.
        /// </summary>
        public SceneViewportNavigation Navigation { get; private set; }

        /// <summary>
        /// Draw the scene
        /// </summary>
        public void DrawScene()
        {
            Scene.Draw(CurrentCamera);
        }

        /// <summary>
        /// The current camera of the viewport
        /// </summary>
        public Camera CurrentCamera { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to draw FPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draw FPS; otherwise, <c>false</c>.
        /// </value>
        public bool FPSEnabled { get; set; }

        /// <summary>
        /// Draw FPS value
        /// </summary>
        /// <param name="frameTime"></param>
        public void DrawFPS(double frameTime)
        {
            if(!FPSEnabled)
                return;
            Scene.OpenGL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f, string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
            Scene.OpenGL.Flush();
        }

        /// <summary>
        /// Change a size of the scene. This affects all viewports
        /// </summary>
        /// <param name="width">Width of the scene</param>
        /// <param name="height">HEight of the scene</param>
        public void ResizeScene(int width, int height)
        {
            //  Get the dimensions.
            if (_lastSizeChangedHeight == height && _lastSizeChangedWidth == width)//avoid repeating resizing calls
                return;
            if (width <= MinSceneWidth || height <= MinSceneHeight)//limit the minimum size of the scene.
                return;
            _lastSizeChangedWidth = width;
            _lastSizeChangedHeight = height;
            Scene.OpenGL.SetDimensions(width, height);
            Scene.Resize(width, height);
        }
    }

    /// <summary>
    /// The interface of the scene viewport
    /// </summary>
    public interface ISceneViewport
    {
        /// <summary>
        /// The current camera of the viewport
        /// </summary>
        Camera CurrentCamera { get; set; }
    }
}