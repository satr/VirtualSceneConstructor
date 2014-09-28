using SharpGL.SceneGraph.Cameras;
using SharpGL.WPF;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    internal class SetCameraToSceneViewCommand : CommandBase
    {
        private readonly SceneView _sceneView;
        private readonly Camera _camera;

        public SetCameraToSceneViewCommand(SceneView sceneView, Camera camera)
        {
            _sceneView = sceneView;
            _camera = camera;
        }

        protected override void Execute()
        {
            _sceneView.Camera = _camera;
        }
    }
}