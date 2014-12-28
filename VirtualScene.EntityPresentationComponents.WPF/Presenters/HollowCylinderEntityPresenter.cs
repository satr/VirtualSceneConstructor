using System.Windows;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the <see cref="HollowCylinderEntity" /> on UI
    /// </summary>
    public class HollowCylinderEntityPresenter : EntityPresenterBase<HollowCylinderEntity>
    {
        /// <summary>
        /// Build the content view for <see cref="HollowCylinderEntity" />.
        /// </summary>
        /// <returns>The view displaying content of the <see cref="HollowCylinderEntity" />.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new HollowCylinderEntityContentView(new SceneEntityContentViewModel<HollowCylinderEntity>(SceneContent));
        }
    }
}