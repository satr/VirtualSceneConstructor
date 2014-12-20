using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// Creates instances of the SceneNavigator
    /// </summary>
    public class SceneNavigationFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ISceneNavigator Create()
        {
            return new SceneNavigator();
        }
    }
}