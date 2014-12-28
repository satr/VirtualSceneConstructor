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
        /// Build the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new SceneViewportView(new SceneViewportViewModel(SceneContent));
        }
    }
}