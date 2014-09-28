using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class CommandWithSceneContentBase : CommandBase
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sceneContent"></param>
        protected CommandWithSceneContentBase(SceneContent sceneContent)
        {
            SceneContent = sceneContent;
            BusinessManager = new BusinessManager();
        }

        /// <summary>
        /// Busimess manager
        /// </summary>
        protected BusinessManager BusinessManager { get; private set; }

        /// <summary>
        /// VirtualSceneContext 
        /// </summary>
        protected SceneContent SceneContent { get; set; }
    }
}