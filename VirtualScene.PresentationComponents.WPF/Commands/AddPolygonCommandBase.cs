using SharpGL.SceneGraph;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// Adds a new scene element to a scene
    /// </summary>
    public abstract class AddPolygonCommandBase : CommandBase
    {
        /// <summary>
        /// Busimess manager
        /// </summary>
        protected BusinessManager BusinessManager { get; private set; }

        /// <summary>
        /// The scene
        /// </summary>
        protected Scene Scene
        {
            get { return SceneContent.SceneEngine.Scene; }
        }

        /// <summary>
        /// Creates a new instance of the AddSphereCommand
        /// </summary>
        /// <param name="sceneContent"></param>
        protected AddPolygonCommandBase(SceneContent sceneContent): base(sceneContent)
        {
            BusinessManager = new BusinessManager();
        }
    }
}