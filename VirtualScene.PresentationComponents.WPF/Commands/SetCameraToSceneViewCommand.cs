using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    internal class SetCameraToSceneViewCommand : CommandBase
    {
        private readonly SceneViewport _sceneViewport;
        private readonly Camera _camera;

        public SetCameraToSceneViewCommand(SceneViewport sceneViewport, Camera camera)
        {
            _sceneViewport = sceneViewport;
            _camera = camera;
        }

        protected override void Execute()
        {
            _sceneViewport.CurrentCamera = _camera;
        }
    }
}