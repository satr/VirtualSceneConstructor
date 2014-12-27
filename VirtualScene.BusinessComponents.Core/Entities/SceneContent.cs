using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VirtualScene.BusinessComponents.Core.Events;
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
        private readonly IDictionary<Type, ICollection<ICollectionChangedSubscriber>> _selectedItemChangedSubscribers = new Dictionary<Type, ICollection<ICollectionChangedSubscriber>>();
        private readonly object _subscribeSyncRoot = new object();

        /// <summary>
        /// Occures when the <seealso cref="SceneContent.Stage" /> is changed.
        /// </summary>
        public event EventHandler<IStage> StageChanged;

        /// <summary>
        /// Occures when the <seealso cref="SceneContent.Stage" /> is changed.
        /// </summary>
        public event EventHandler<IEnumerable<ISceneEntity>> SelectedSceneElementsChanged;

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
        /// The collection of items selected in the scene.
        /// </summary>
        public IEnumerable<ISceneEntity> SelectedItems
        {
            get { return _selectedItems; }
        }

        /// <summary>
        /// Set the collection of selected items.
        /// </summary>
        /// <param name="items">The collection of <see cref="ISceneEntity" /></param>
        public void SetSelectedItems(IEnumerable<ISceneEntity> items)
        {
            _selectedItems.Clear();
            if (items != null)
            {
                foreach (var entity in items)
                    _selectedItems.Add(entity);
            }
            OnStageSelectedItemsChanged();
        }

        /// <summary>
        /// Subscribe on the action when items of specified type are selected.
        /// </summary>
        /// <param name="subscriber">The subcriber to be notified about the operation.</param>
        /// <typeparam name="T">The type of selected items.</typeparam>
        public void SubscribeOnSelectedItems<T>(ICollectionChangedSubscriber subscriber)
        {
            lock (_subscribeSyncRoot)
            {
                if(!_selectedItemChangedSubscribers.ContainsKey(typeof(T)))
                    _selectedItemChangedSubscribers.Add(typeof(T), new List<ICollectionChangedSubscriber>());
                _selectedItemChangedSubscribers[typeof (T)].Add(subscriber);
            }
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
                ReplaceStage(value);
                OnStageSelectedItemsChanged();
                OnStageChanged(_stage);
            }
        }

        private void ReplaceStage(IStage value)
        {
            if (_stage != null)
                _stage.Items.CollectionChanged -= OnSceneEntityCollectionChanged;
            _sceneEngine.Clear();
            _selectedItems.Clear();
            _stage = value;
            foreach (var sceneEntity in _stage.Items)
                _sceneEngine.AddSceneEntity(sceneEntity);
            if (_stage != null)
                _stage.Items.CollectionChanged += OnSceneEntityCollectionChanged;
        }

        private void OnSceneEntityCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
                RemoveSceneEntities(args);
            
            if (args.NewItems != null)
                AddSceneEntities(args.NewItems);
        }

        private void RemoveSceneEntities(NotifyCollectionChangedEventArgs args)
        {
            var selectedItemsRemoved = false;
            foreach (ISceneEntity sceneEntity in args.OldItems)
            {
                _sceneEngine.RemoveSceneEntity(sceneEntity);
                if (!_selectedItems.Contains(sceneEntity)) 
                    continue;
                _selectedItems.Remove(sceneEntity);
                selectedItemsRemoved = true;
            }
            if(selectedItemsRemoved)
                OnStageSelectedItemsChanged();
        }

        private void AddSceneEntities(IEnumerable sceneEntities)
        {
            foreach (ISceneEntity sceneEntity in sceneEntities)
                _sceneEngine.AddSceneEntity(sceneEntity);
        }

        private void OnStageChanged(IStage stage)
        {
            var handler = StageChanged;
            if (handler != null) 
                handler(this, stage);
        }

        private void OnStageSelectedItemsChanged()
        {
            var handler = SelectedSceneElementsChanged;
            if (handler != null) 
                handler(this, SelectedItems);
        }
    }
}
