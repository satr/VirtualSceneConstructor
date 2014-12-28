using System.Windows;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the <see cref="SpurGearEntity" /> on UI
    /// </summary>
    public class SpurGearEntityPresenter : EntityPresenterBase<SpurGearEntity>
    {
        /// <summary>
        /// Build the content view for <see cref="SpurGearEntity" />.
        /// </summary>
        /// <returns>The view displaying content of the <see cref="SpurGearEntity" />.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new SpurGearEntityContentView(new SceneEntityContentViewModel<SpurGearEntity>(SceneContent));
        }
    }
}