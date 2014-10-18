namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Interface to the content of the scene
    /// </summary>
    public interface ISceneContent
    {
        /// <summary>
        /// The instance of the SceneEngine
        /// </summary>
        ISceneEngine SceneEngine { get; }

        /// <summary>
        /// The stage of the 3D environment
        /// </summary>
        IStage Stage { get; set; }

        /// <summary>
        /// Navigation in the scene
        /// </summary>
        ISceneNavigation Navigation { get; set; }
    }
}