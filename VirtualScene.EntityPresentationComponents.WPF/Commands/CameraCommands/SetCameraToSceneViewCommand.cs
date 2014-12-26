using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.CameraCommands
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

        protected override void ExecuteAction(object parameter)
        {
            _sceneViewport.CurrentCamera = _camera;
        }
    }
}