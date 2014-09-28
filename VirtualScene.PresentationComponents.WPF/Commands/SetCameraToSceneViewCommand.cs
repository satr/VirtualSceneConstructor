using SharpGL.SceneGraph.Cameras;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    internal class SetCameraToSceneViewCommand : CommandBase
    {
        private readonly SceneViewControl _sceneViewControl;
        private readonly Camera _camera;

        public SetCameraToSceneViewCommand(SceneViewControl sceneViewControl, Camera camera)
        {
            _sceneViewControl = sceneViewControl;
            _camera = camera;
        }

        protected override void Execute()
        {
            _sceneViewControl.Camera = _camera;
        }
    }
}