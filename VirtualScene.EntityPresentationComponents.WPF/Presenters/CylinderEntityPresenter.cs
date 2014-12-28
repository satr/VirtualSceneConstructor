using System.Windows;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the <see cref="CylinderEntity" /> on UI
    /// </summary>
    public class CylinderEntityPresenter : EntityPresenterBase<CylinderEntity>
    {
        /// <summary>
        /// Build the content view for <see cref="CylinderEntity" />.
        /// </summary>
        /// <returns>The view displaying content of the <see cref="CylinderEntity" />.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new CylinderEntityContentView(new SceneEntityContentViewModel<CylinderEntity>(SceneContent));
        }
    }
}