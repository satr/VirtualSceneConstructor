using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.Version;
using VirtualScene.BusinessComponents.Core.Validators;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Factory creating virtual scenes
    /// </summary>
    public class VirtualSceneFactory
    {
        private readonly OpenGL _gl = new OpenGL();
        private readonly Vertex _defaultCameraPosition = new Vertex(-10, -10, 10);

        /// <summary>
        /// Creates a new virtual scene
        /// </summary>
        /// <param name="width">Width of the virtual scene</param>
        /// <param name="height">Height of the virtual scene</param>
        /// <param name="bitDepth">Color depth of the virtual scene</param>
        /// <returns></returns>
        public Scene Create(int width = Constants.VirtualScene.MinimumWidth, int height = Constants.VirtualScene.MinimumHeight, int bitDepth = Constants.VirtualScene.MaximumColorDepth)
        {
            VirtualSceneArgumentValidator.ValidateSceneArguments(width, height, bitDepth);
            _gl.Create(OpenGLVersion.OpenGL4_4, RenderContextType.DIBSection, width, height, bitDepth, null);
            var scene = new Scene { OpenGL = _gl };
            SharpGL.SceneGraph.Helpers.SceneHelper.InitialiseModelingScene(scene);
            scene.CurrentCamera = CameraFactory.Create<ArcBallCamera>(_defaultCameraPosition);
            return scene;
        }
    }
}