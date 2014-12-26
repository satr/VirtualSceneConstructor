using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// Base command for scene content and scene objects related operations
    /// </summary>
    public abstract class CommandWithSceneContentBase : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the base command for scene content and scene objects related operations
        /// </summary>
        /// <param name="sceneContent"></param>
        protected CommandWithSceneContentBase(ISceneContent sceneContent)
        {
            SceneContent = sceneContent;
        }

        /// <summary>
        /// VirtualSceneContext 
        /// </summary>
        protected ISceneContent SceneContent { get; set; }
    }
}