using System.Windows;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the <see cref="CubeEntity" /> on UI
    /// </summary>
    public class CubeEntityPresenter : EntityPresenterBase<CubeEntity>
    {
        /// <summary>
        /// Build the content view for <see cref="CubeEntity" />.
        /// </summary>
        /// <returns>The view displaying content of the <see cref="CubeEntity" />.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new CubeEntityContentView(new SceneEntityContentViewModel<CubeEntity>(SceneContent));
        }
    }
}