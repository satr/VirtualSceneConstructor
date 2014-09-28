using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command adding a new camera
    /// </summary>
    /// <typeparam name="T">Type of the camera</typeparam>
    public class AddCameraCommand<T> : AddSceneObjectCommandBase
        where T: Camera, new()
    {
        /// <summary>
        /// Create a new instance of the command
        /// </summary>
        /// <param name="sceneContent">Scene Content</param>
        public AddCameraCommand(SceneContent sceneContent) : base(sceneContent)
        {
        }

        /// <summary>
        /// Exectute adding of the new command
        /// </summary>
        protected override void Execute()
        {
            SceneEngine.Cameras.Add(CameraFactory.Create<T>(Constants.SceneEngine.DefaultCameraVector));
        }
    }
}