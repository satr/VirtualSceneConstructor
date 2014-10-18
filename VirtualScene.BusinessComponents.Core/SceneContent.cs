using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core.Factories;

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
                    _stage.Entities.CollectionChanged -= _sceneEngine.StageEntitiesChanged;
                _stage = value;
                _sceneEngine.Clear();
                if (_stage != null)
                    _stage.Entities.CollectionChanged += _sceneEngine.StageEntitiesChanged;
            }
        }
    }
}
