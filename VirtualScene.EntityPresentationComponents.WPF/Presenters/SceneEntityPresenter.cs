using System.Windows;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Presenters;
using VirtualScene.PresentationComponents.WPF.ViewModels;
using SceneEntityContentView = VirtualScene.EntityPresentationComponents.WPF.Views.SceneEntityContentView;

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