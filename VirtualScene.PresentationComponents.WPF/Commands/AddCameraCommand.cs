using System.Reflection;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.PresentationComponents.WPF.Models;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command adding a new camera
    /// </summary>
    /// <typeparam name="T">Type of the camera</typeparam>
    public class AddCameraCommand<T> : AddSceneObjectCommandBase
        where T: Camera, new()
    {
        private string _title;

        /// <summary>
        /// Create a new instance of the command
        /// </summary>
        /// <param name="sceneContent">Content od the scene</param>
        /// <param name="title"></param>
        public AddCameraCommand(SceneContent sceneContent, string title) : base(sceneContent)
        {
            _title = title;
        }

        /// <summary>
        /// Adding of the new camera
        /// </summary>
        protected override void Execute()
        {
            var entityNameViewModel = new EntityNameViewModel { Name = CreateCameraNameBasedOnCameraType() };
            _title = Properties.Resources.Title_Add_Arc_Camera;
            var dialogResult = new EntityNameDialogView(_title, entityNameViewModel).ShowDialog();
            if(!dialogResult.HasValue || !dialogResult.Value)
                return;
            SceneEngine.Cameras.Add(CameraFactory.Create<T>(Constants.SceneEngine.DefaultCameraVector, entityNameViewModel.Name));
        }

        private static string CreateCameraNameBasedOnCameraType()
        {
            return string.Format(Properties.Resources.Title_Camera_N, typeof(T).GetTypeInfo().Name);
        }
    }
}