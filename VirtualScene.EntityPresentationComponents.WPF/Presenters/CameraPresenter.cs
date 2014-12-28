using System.Collections.Generic;
using System.Windows;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CameraCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents the camera on UI
    /// </summary>
    public class CameraPresenter : EntityPresenterBase<Camera>
    {
        /// <summary>
        /// Build the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new CameraContentView();
        }

        /// <summary>
        /// Control elements to operate with the entity
        /// </summary>
        public override IEnumerable<UIElement> TopElements
        {
            get
            {
                yield return CreateButton(Resources.Title_Add_Arc_Camera, new AddCameraCommand<ArcBallCamera>(SceneContent, Resources.Title_Add_Arc_Camera));
                yield return CreateButton(Resources.Title_Add_Free_Camera, new AddCameraCommand<LookAtCamera>(SceneContent, Resources.Title_Add_Free_Camera));
            }
        }
    }
}