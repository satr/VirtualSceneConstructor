using System.Reflection;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.CameraCommands
{
    /// <summary>
    /// The command adding a new camera
    /// </summary>
    /// <typeparam name="T">Type of the camera</typeparam>
    public class AddCameraCommand<T> : AddSceneObjectCommandBase
        where T: Camera, new()
    {
        private readonly string _title;

        /// <summary>
        /// Create a new instance of the command
        /// </summary>
        /// <param name="sceneContent">Content od the scene</param>
        /// <param name="title"></param>
        public AddCameraCommand(ISceneContent sceneContent, string title) : base(sceneContent)
        {
            _title = title;
        }

        /// <summary>
        /// Adding of the new camera
        /// </summary>
        protected override void Execute()
        {
            var viewModel = new EntityNameDialogViewModel(_title, CreateCameraNameBasedOnCameraType());
            new EntityNameDialogView(viewModel).ShowDialog();
            if (viewModel.OperationCancelled)
                return;
            SceneEngine.Cameras.Add(ServiceLocator.Get<CameraFactory>().Create<T>(Constants.SceneEngine.DefaultCameraVector, viewModel.Name));
        }

        private static string CreateCameraNameBasedOnCameraType()
        {
            return string.Format(Resources.Title_Camera_N, typeof(T).GetTypeInfo().Name);
        }
    }
}