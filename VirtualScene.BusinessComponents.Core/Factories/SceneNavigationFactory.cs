namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// Creates instances of the SceneNavigation
    /// </summary>
    public class SceneNavigationFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ISceneNavigation Create()
        {
            return new SceneNavigation();
        }
    }
}