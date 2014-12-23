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
        /// Content of the scene-entity
        /// </summary>
        /// <returns>The view with the content of the scene-entity</returns>
        public override FrameworkElement GetContentView()
        {
            return new SceneEntityContentView(new SceneEntityContentViewModel(SceneContent));
        }
    }
}