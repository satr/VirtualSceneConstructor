using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VirtualScene.BusinessComponents.Core.Controllers;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// The content of the scene
    /// </summary>
    public class SceneContent : ISceneContent
    {
        private IStage _stage;
        private readonly ObservableCollection<ISceneEntity> _selectedItems = new ObservableCollection<ISceneEntity>();
        private readonly CollectionNotificationControllerManager _collectionNotificationControllerManager = new CollectionNotificationControllerManager();

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
        /// <param name="sceneEngine">The <see cref="ISceneEngine" /> performing operations with the scene.</param>
        public SceneContent(ISceneEngine sceneEngine)
        {
            SceneEngine = sceneEngine;
            Navigator = ServiceLocator.Get<SceneNavigationFactory>().Create();
            Navigator.Move += (s, e) => SceneEngine.Move(e.X, e.Y, e.Z);
        }

        /// <summary>
        /// The collection of items selected in the scene.
        /// </summary>
        public IEnumerable<ISceneEntity> SelectedItems
        {
            get { return _selectedItems; }
        }

        /// <summary>
        /// Set the collection of selected <see cref="ISceneEntity" />.
        /// </summary>
        /// <param name="items">The collection of selected <see cref="ISceneEntity" />.</param>
        public void SetSelectedItems(IEnumerable<ISceneEntity> items)
        {
            _selectedItems.Clear();
            if (items == null)
            {
                OnStageSelectedItemsChanged();
                return;
            }

            var selectedSceneEntities = items.Where(entity => entity != null).ToList();
            var entityTypes = SetSelectedItems(_selectedItems, selectedSceneEntities);
            OnStageSelectedItemsChanged();
            if (entityTypes.Count == 1)
                NotifySubscribersOnSelectedItems(entityTypes[0], selectedSceneEntities);
        }

        private static List<Type> SetSelectedItems(ICollection<ISceneEntity> sceneEntities, IEnumerable<ISceneEntity> entities)
        {
            var entityTypes = new List<Type>();
            foreach (var entity in entities)
            {
                sceneEntities.Add(entity);
                var type = entity.GetType();
                if (!entityTypes.Contains(type))
                    entityTypes.Add(type);
            }
            return entityTypes;
        }

        private void NotifySubscribersOnSelectedItems(Type entityType, ICollection<ISceneEntity> sceneEntities)
        {
            _collectionNotificationControllerManager.Notify(entityType, sceneEntities);
        }

        /// <summary>
        /// Subscribe on the action when items of specified type are selected.
        /// </summary>
        /// <param name="subscriber">The subcriber to be notified about the operation.</param>
        /// <typeparam name="T">The type of selected items.</typeparam>
        public void SubscribeOnSelectedItems<T>(ICollectionItemsOperationSubscriber subscriber)
        {
            _collectionNotificationControllerManager.Add<T>(subscriber);
        }

        /// <summary>
        /// Navigation in the scene
        /// </summary>
        public ISceneNavigator Navigator { get; set; }

        /// <summary>
        /// The instance of the SceneEngine
        /// </summary>
        public ISceneEngine SceneEngine { get; private set; }

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
                ReplaceCurrentStage(value);
                OnStageSelectedItemsChanged();
                OnStageChanged(_stage);
            }
        }

        private void ReplaceCurrentStage(IStage stage)
        {
            UnBind(_stage);
            SceneEngine.Clear();
            _selectedItems.Clear();
            _stage = stage;
            AddSceneEntitiesToScene(_stage.Items);
            Bind(_stage);
        }

        private void AddSceneEntitiesToScene(IEnumerable<ISceneEntity> sceneEntities)
        {
            foreach (var sceneEntity in sceneEntities)
                SceneEngine.AddSceneEntity(sceneEntity);
        }

        private void Bind(IStage stage)
        {
            if (stage == null)
                return;
            stage.SceneEntityAdded += StageOnSceneEntityAdded;
            stage.SceneEntityRemoved += StageOnSceneEntityRemoved;
        }

        private void UnBind(IStage stage)
        {
            if (stage == null) 
                return;
            stage.SceneEntityAdded -= StageOnSceneEntityAdded;
            stage.SceneEntityRemoved -= StageOnSceneEntityRemoved;
        }

        private void StageOnSceneEntityRemoved(object sender, ISceneEntity sceneEntity)
        {
            if (sceneEntity != null)
                RemoveSceneEntities(sceneEntity);
        }

        private void StageOnSceneEntityAdded(object sender, ISceneEntity sceneEntity)
        {
            if (sceneEntity != null)
                SceneEngine.AddSceneEntity(sceneEntity);
        }

        private void RemoveSceneEntities(ISceneEntity sceneEntity)
        {
            SceneEngine.RemoveSceneEntity(sceneEntity);
            if (!_selectedItems.Contains(sceneEntity)) 
                return;
            _selectedItems.Remove(sceneEntity);
            OnStageSelectedItemsChanged();
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
