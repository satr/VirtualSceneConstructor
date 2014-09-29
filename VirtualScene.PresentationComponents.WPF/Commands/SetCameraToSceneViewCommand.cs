using SharpGL.SceneGraph.Cameras;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    internal class SetCameraToSceneViewCommand : CommandBase
    {
        private readonly SceneViewModel _sceneViewModel;
        private readonly Camera _camera;

        public SetCameraToSceneViewCommand(SceneViewModel sceneViewModel, Camera camera)
        {
            _sceneViewModel = sceneViewModel;
            _camera = camera;
        }

        protected override void Execute()
        {
            _sceneViewModel.Camera = _camera;
        }
    }
}