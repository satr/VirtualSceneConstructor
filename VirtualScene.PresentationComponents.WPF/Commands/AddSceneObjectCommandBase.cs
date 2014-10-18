using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// Adds a new scene element to a scene
    /// </summary>
    public abstract class AddSceneObjectCommandBase : CommandWithSceneContentBase
    {
        /// <summary>
        /// Creates a new instance of the AddSphereCommand
        /// </summary>
        /// <param name="sceneContent"></param>
        protected AddSceneObjectCommandBase(ISceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// The SceneEngine
        /// </summary>
        protected ISceneEngine SceneEngine
        {
            get { return SceneContent.SceneEngine; }
        }
    }
}