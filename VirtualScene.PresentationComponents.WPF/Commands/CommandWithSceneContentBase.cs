using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// Base command for scene content and scene objects related operations
    /// </summary>
    public abstract class CommandWithSceneContentBase : CommandBase
    {
        /// <summary>
        /// Creates a new instance of the base command for scene content and scene objects related operations
        /// </summary>
        /// <param name="sceneContent"></param>
        protected CommandWithSceneContentBase(SceneContent sceneContent)
        {
            SceneContent = sceneContent;
        }

        /// <summary>
        /// VirtualSceneContext 
        /// </summary>
        protected SceneContent SceneContent { get; set; }
    }
}