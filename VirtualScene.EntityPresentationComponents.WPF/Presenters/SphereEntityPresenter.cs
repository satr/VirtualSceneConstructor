using System.Windows;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the <see cref="SphereEntity" /> on UI
    /// </summary>
    public class SphereEntityPresenter : EntityPresenterBase<SphereEntity>
    {
        /// <summary>
        /// Build the content view for <see cref="SphereEntity" />.
        /// </summary>
        /// <returns>The view displaying content of the <see cref="SphereEntity" />.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new SphereEntityContentView(new SceneEntityContentViewModel<SphereEntity>(SceneContent));
        }
    }
}