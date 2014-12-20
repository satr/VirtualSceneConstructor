using System;
using System.Collections.Specialized;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.BusinessComponents.Core.Properties;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.Core
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
            Navigation = ServiceLocator.Get<SceneNavigationFactory>().Create();
            Navigation.Move += (s, e) => _sceneEngine.Move(e.X, e.Y, e.Z);
        }

        /// <summary>
        /// Navigation in the scene
        /// </summary>
        public ISceneNavigation Navigation { get; set; }

        /// <summary>
        /// Add a new entity to the current stage of the scene.
        /// </summary>
        /// <param name="sceneEntity"></param>
        /// <exception cref="InvalidOperationException">The exception is thrown when a stage is not initialised - it is null</exception>
        public void Add(ISceneEntity sceneEntity)
        {
            if (Stage == null)
                throw new InvalidOperationException(Resources.Title_Add_The_stage_of_the_scene_is_not_initialized);
            Stage.Items.Add(sceneEntity);
        }

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
                foreach (ISceneEntity item in args.NewItems)
                {
                    _sceneEngine.AddSceneEntity(item);
                }
            }
        }
    }
}
