using System.Windows;
using VirtualScene.Entities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the scene-entity on UI
    /// </summary>
    public class SceneEntityPresenter : EntityPresenterBase<ISceneEntity>
    {
        /// <summary>
        /// Create the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new SceneEntityContentView(new SceneEntityContentViewModel(SceneContent));
        }
    }
}