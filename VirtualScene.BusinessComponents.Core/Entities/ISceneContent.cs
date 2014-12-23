using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.Core.Entities
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
        ISceneNavigator Navigator { get; set; }
    }
}