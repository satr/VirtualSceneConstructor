namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The content of the scene
    /// </summary>
    public class SceneContent
    {
        private readonly SceneEngine _sceneEngine;

        /// <summary>
        /// Creates a new instance of the content of the virtual scene
        /// </summary>
        public SceneContent()
        {
            _sceneEngine = new SceneEngine();
        }

        /// <summary>
        /// The instance of the SceneEngine
        /// </summary>
        public SceneEngine SceneEngine
        {
            get { return _sceneEngine; }
        }
    }
}
