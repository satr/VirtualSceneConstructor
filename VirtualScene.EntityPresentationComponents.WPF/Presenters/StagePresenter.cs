using System.Windows;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the stage on UI
    /// </summary>
    public class StagePresenter : EntityPresenterBase<Stage>
    {
        /// <summary>
        /// Content of the stage
        /// </summary>
        /// <returns>The view with the content of the stage</returns>
        public override FrameworkElement GetContentView()
        {
            return new StageContentView(new StageContentViewModel(SceneContent));
        }
    }
}
