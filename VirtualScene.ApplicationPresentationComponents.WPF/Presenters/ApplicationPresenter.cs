using System;
using System.Collections.Generic;
using System.Windows;
using VirtualScene.ApplicationPresentationComponents.WPF.Commands;
using VirtualScene.ApplicationPresentationComponents.WPF.Properties;
using VirtualScene.ApplicationPresentationComponents.WPF.Views;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Common;
using VirtualScene.EntityPresentationComponents.WPF.Presenters;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.ApplicationPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// ApplicationPresenter provides UI components for application
    /// </summary>
    public class ApplicationPresenter: EntityPresenterBase<Stage>
    {
        private readonly FrameworkElement _stageContentView;
        private readonly Dictionary<Type, IEntityPresenter> _entityPresenters = new Dictionary<Type, IEntityPresenter>();
        private readonly List<IContentPresenter> _contentPresenters = new List<IContentPresenter>();
        private readonly FrameworkElement _sceneEntityDetailView;
        private readonly IContentPresenter _viewportPresenter1;
        private readonly IContentPresenter _viewportPresenter2;
        private readonly IContentPresenter _viewportPresenter3;

        /// <summary>
        /// Creates a new instance of the presenter
        /// </summary>
        public ApplicationPresenter()
        {
            SceneContent = ServiceLocator.Get<SceneContentFactory>().Create();
            var stagePresenter = RegisterEntityPresenter<StagePresenter>();
            RegisterEntityPresenter<CameraPresenter>();
            RegisterEntityPresenter<GeometryPrimitivePresenter>();
            var sceneEntityPresenter = RegisterEntityPresenter<SceneEntityPresenter>();
            _viewportPresenter1 = RegisterContentPresenter<SceneViewportPresenter>();
            _viewportPresenter2 = RegisterContentPresenter<SceneViewportPresenter>();
            _viewportPresenter3 = RegisterContentPresenter<SceneViewportPresenter>();
            SceneContent.Stage = new Stage();
            _stageContentView = stagePresenter.GetContentView();
            _sceneEntityDetailView = sceneEntityPresenter.GetContentView();
        }

        private IEntityPresenter RegisterEntityPresenter<T>()
            where T: IEntityPresenter, new()
        {
            var presenter = new T();
            if(_entityPresenters.ContainsKey(presenter.EntityType))
                throw new InvalidOperationException(string.Format(Resources.Message_Presenter_for_the_type_N_has_been_already_added, presenter.EntityType));
            _entityPresenters.Add(presenter.EntityType, presenter);
            presenter.SceneContent = SceneContent;
            return presenter;
        }
        private IContentPresenter RegisterContentPresenter<T>()
            where T: IContentPresenter, new()
        {
            var presenter = new T();
            _contentPresenters.Add(presenter);
            presenter.SceneContent = SceneContent;
            return presenter;
        }

        /// <summary>
        /// Elements Locate at the top of main window
        /// </summary>
        public override IEnumerable<UIElement> TopElements
        {
            get
            {
                var topElements = new List<UIElement>();
                foreach (var presenter in _entityPresenters.Values)
                {
                    topElements.AddRange(presenter.TopElements);
                }
                topElements.Add(CreateButton(Resources.Title_Help, new ShowHelpCommand()));
                return topElements;
            }
        }

        /// <summary>
        /// The view displaying the content of the stage
        /// </summary>
        /// <returns>The view</returns>
        public FrameworkElement GetStageContentView()
        {
            return _stageContentView;
        }

        /// <summary>
        /// The content view of the main entity
        /// </summary>
        /// <returns>The view with content</returns>
        public override FrameworkElement GetContentView()
        {
            return new MainEntityContentView();
        }

        /// <summary>
        /// TODO temporary show some entity content
        /// </summary>
        /// <returns></returns>
        public FrameworkElement GetDetailView()
        {
            return _sceneEntityDetailView;
        }

        /// <summary>
        /// Viewport
        /// </summary>
        /// <returns></returns>
        public FrameworkElement Get3DViewport1()
        {
            return _viewportPresenter1.GetContentView();
        }

        /// <summary>
        /// Viewport
        /// </summary>
        /// <returns></returns>
        public FrameworkElement Get3DViewport2()
        {
            return _viewportPresenter2.GetContentView();
        }

        /// <summary>
        /// Viewport
        /// </summary>
        /// <returns></returns>
        public FrameworkElement Get3DViewport3()
        {
            return _viewportPresenter3.GetContentView();
        }
    }
}