namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// The factory creating a scene-engines
    /// </summary>
    public class SceneEngineFactory
    {
        /// <summary>
        /// Create a new instance of the scene-engine
        /// </summary>
        /// <returns>The new instance of the </returns>
        public virtual ISceneEngine Create()
        {
            return new SceneEngine();
        }
    }
}
