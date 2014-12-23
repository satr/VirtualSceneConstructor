using System.Collections;
using System.Collections.Generic;
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

        /// <summary>
        /// Creates a new instance of the content of the virtual scene
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
                _stage = value;
                _sceneEngine.Clear();
                foreach (var sceneEntity in _stage.Items)
                    _sceneEngine.AddSceneEntity(sceneEntity);
                if (_stage != null)
                    _stage.Items.CollectionChanged += EntitiesOnCollectionChanged;
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
    }
}
