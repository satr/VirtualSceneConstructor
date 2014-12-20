using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// Create instances of scene-content
    /// </summary>
    public class SceneContentFactory
    {
        /// <summary>
        /// Create a new instance of the scene-content
        /// </summary>
        /// <returns>The new instance of the scene-content</returns>
        public virtual ISceneContent Create()
        {
            return new SceneContent();
        }
    }
}
