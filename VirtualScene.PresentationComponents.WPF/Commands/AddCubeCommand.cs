using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command creates a cube and adds it to the scene
    /// </summary>
    public class AddCubeCommand: CommandBase
    {
        private int _x = 0;
        private int _y = 0;
        /// <summary>
        /// Creates a new instance of the AddCubeCommand
        /// </summary>
        /// <param name="sceneContent"></param>
        public AddCubeCommand(SceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected override void Execute()
        {
            var item = new Cube();
            _x += 1;
            _y += 1;
            item.Transformation.TranslateX += _x;
            item.Transformation.TranslateY += _y;
            SceneContent.SceneEngine.CommonSceneContainer.Add(item);
        }
    }
}
