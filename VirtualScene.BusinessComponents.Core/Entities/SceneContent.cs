using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Common;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// The content of the scene
    /// </summary>
    public class SceneContent : ISceneContent
    {
        private readonly ISceneEngine _sceneEngine;
        private IStage _stage;
        private readonly ObservableCollection<ISceneEntity> _selectedItems = new ObservableCollection<ISceneEntity>();

        /// <summary>
        /// The collection of items selected in the scene.
        /// </summary>
        public ObservableCollection<ISceneEntity> SelectedItems
        {
            get { return _selectedItems; }
        }

        /// <summary>
        /// Occures when the <seealso cref="SceneContent.Stage" /> is changed.
        /// </summary>
        public event EventHandler<IStage> StageChanged;

        /// <summary>
        /// Set the collection of selected items.
        /// </summary>
        /// <param name="items">The collection of <see cref="ISceneEntity" /></param>
        public void SetSelectedItems(IEnumerable<ISceneEntity> items)
        {
            _selectedItems.Clear();
            if (items == null)
                return;
            foreach (var entity in items)
                _selectedItems.Add(entity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContent" />
        /// </summary>
        public SceneContent()
        {
            _sceneEngine = ServiceLocator.Get<SceneEngineFactory>().Create();
            Navigator = ServiceLocator.Get<SceneNavigationFactory>().Create();
            Navigator.Move += (s, e) => _sceneEngine.Move(e.X, e.Y, e.Z);
        }

        /// <summary>
        /// Navigation in the scene
        /// </summary>
        public ISceneNavigator Navigator { get; set; }

        /// <summary>
        /// The instance of the SceneEngine
        /// </summary>
        public ISceneEngine SceneEngine
        {
            get { return _sceneEngine; }
        }

        /// <summary>
        /// The stage of the 3D environment
        /// </summary>
        public IStage Stage
        {
            get { return _stage; }
            set
            {
                if(Equals(_stage, value))
                    return;
                if (_stage != null)
                    _stage.Items.CollectionChanged -= EntitiesOnCollectionChanged;
                _sceneEngine.Clear();
                _stage = value;
                foreach (var sceneEntity in _stage.Items)
                    _sceneEngine.AddSceneEntity(sceneEntity);
                if (_stage != null)
                    _stage.Items.CollectionChanged += EntitiesOnCollectionChanged;
                OnStageChanged(_stage);
            }
        }

        private void EntitiesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if(args.OldItems != null)
            {
                foreach (ISceneEntity item in args.OldItems)
                {
                    _sceneEngine.RemoveSceneEntity(item);
                }
            }
            if(args.NewItems != null)
            {
                AddSceneEntities(args.NewItems);
            }
        }

        private void AddSceneEntities(IList sceneEntities)
        {
            foreach (ISceneEntity item in sceneEntities)
            {
                _sceneEngine.AddSceneEntity(item);
            }
        }

        private void OnStageChanged(IStage stage)
        {
            var handler = StageChanged;
            if (handler != null) 
                handler(this, stage);
        }
    }
}
