using System.Collections.Generic;
using System.Windows;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.EntityPresentationComponents.WPF.Commands.StageCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

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

        /// <summary>
        /// Control elements for stage operations
        /// </summary>
        public override IEnumerable<UIElement> TopElements
        {
            get { yield return CreateButton(Resources.Title_Save_Stage, new SaveStageCommand(SceneContent)); }
        }
    }
}
