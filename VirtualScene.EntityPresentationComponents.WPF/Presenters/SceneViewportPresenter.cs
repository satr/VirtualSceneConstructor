using System.Windows;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents the 3D view on UI
    /// </summary>
    public class SceneViewportPresenter : ContentPresenterBase
    {
        /// <summary>
        /// Content of the camera
        /// </summary>
        /// <returns>The view with the content of the 3D viewport</returns>
        public override FrameworkElement GetContentView()
        {
            return new SceneViewportView(new SceneViewportViewModel(SceneContent));
        }
    }
}