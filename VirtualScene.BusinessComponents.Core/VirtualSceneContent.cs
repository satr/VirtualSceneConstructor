namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The content of the virtual scene
    /// </summary>
    public class VirtualSceneContent
    {
        private readonly VirtualSceneEngine _virtualSceneEngine;

        /// <summary>
        /// Creates a new instance of the content of the virtual scene
        /// </summary>
        public VirtualSceneContent()
        {
            _virtualSceneEngine = new VirtualSceneEngine();
        }

        /// <summary>
        /// The instance of the VirtualSceneEngine
        /// </summary>
        public VirtualSceneEngine VirtualSceneEngine
        {
            get { return _virtualSceneEngine; }
        }
    }
}
