using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Properties;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command creates a cube and adds it to the scene
    /// </summary>
    public class AddCubeCommand : AddSceneObjectCommandBase
    {
        private int _x;
        private int _y;
        /// <summary>
        /// Creates a new instance of the AddCubeCommand
        /// </summary>
        /// <param name="sceneContent"></param>
        public AddCubeCommand(ISceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected override void Execute()
        {
            _x += 1;
            _y += 1;
            ServiceLocator.Get<BusinessManager>().AddSceneElementInSpace<Cube>(SceneContent, _x, _y, 0, Resources.Title_Cube);
        }
    }
}
