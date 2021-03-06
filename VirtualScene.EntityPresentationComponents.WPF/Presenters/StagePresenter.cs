﻿using System.Collections.Generic;
using System.Windows;
using VirtualScene.Entities;
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
        /// Build the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new StageContentView(new StageContentViewModel(SceneContent));
        }

        /// <summary>
        /// Control elements for stage operations
        /// </summary>
        public override IEnumerable<UIElement> TopElements
        {
            get
            {
                yield return CreateButton(Resources.Title_Save_Stage, new SaveStageCommand(SceneContent));
                yield return CreateButton(Resources.Title_Load_stage, new LoadStageCommand(SceneContent));
            }
        }
    }
}
