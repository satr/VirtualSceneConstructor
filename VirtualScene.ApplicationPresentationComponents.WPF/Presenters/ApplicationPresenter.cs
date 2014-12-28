using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VirtualScene.ApplicationPresentationComponents.WPF.Commands;
using VirtualScene.ApplicationPresentationComponents.WPF.Properties;
using VirtualScene.ApplicationPresentationComponents.WPF.Views;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;
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
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly List<IContentPresenter> _contentPresenters = new List<IContentPresenter>();
        private readonly IContentPresenter _viewportPresenter1;
        private readonly IContentPresenter _viewportPresenter2;
        private readonly IContentPresenter _viewportPresenter3;

        /// <summary>
        /// Set the detailed view of the item(s) selected in the scene-content
        /// </summary>
        public event EventHandler<FrameworkElement> SetDetailedView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPresenter" />
        /// </summary>
        public ApplicationPresenter()
        {
            SceneContent = new SceneContent(new SceneEngine()) {Stage = new Stage()};

            var stagePresenter = RegisterEntityPresenter<StagePresenter>();
            RegisterEntityPresenter<CameraPresenter>();
            RegisterEntityPresenter<GeometryPrimitivePresenter>();
            RegisterEntityPresenter<CubeEntityPresenter>();
            RegisterEntityPresenter<SphereEntityPresenter>();
            RegisterEntityPresenter<HollowCylinderEntityPresenter>();
            RegisterEntityPresenter<CylinderEntityPresenter>();
            _viewportPresenter1 = RegisterContentPresenter<SceneViewportPresenter>();
            _viewportPresenter2 = RegisterContentPresenter<SceneViewportPresenter>();
            _viewportPresenter3 = RegisterContentPresenter<SceneViewportPresenter>();

            _stageContentView = stagePresenter.GetContentView();
            
            SceneContent.SelectedSceneElementsChanged += SceneContentSelectedSceneElementsChanged;
        }

        private void SceneContentSelectedSceneElementsChanged(object sender, IEnumerable<ISceneEntity> sceneEntities)
        {
            var selectedSceneEntities = sceneEntities.ToList();
            OnSetDetailedView(GetContentViewForSelectedEntity(selectedSceneEntities));
        }

        private FrameworkElement GetContentViewForSelectedEntity(IReadOnlyCollection<ISceneEntity> selectedSceneEntities)
        {
            if (selectedSceneEntities.Count != 1)
                return null;
            var selectedSceneEntityType = selectedSceneEntities.First().GetType();
            return _entityPresenters.ContainsKey(selectedSceneEntityType) ? _entityPresenters[selectedSceneEntityType].GetContentView() : null;
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
        /// Build the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected override FrameworkElement CreateContentView()
        {
            return new MainEntityContentView();
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

        /// <summary>
        /// Invoke the event <see cref="SetDetailedView" />
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSetDetailedView(FrameworkElement e)
        {
            var handler = SetDetailedView;
            if (handler != null) 
                handler(this, e);
        }
    }
}