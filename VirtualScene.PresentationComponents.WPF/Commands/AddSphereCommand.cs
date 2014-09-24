using SharpGL.SceneGraph.Quadrics;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command creates a sphere and adds it to the scene
    /// </summary>
    public class AddSphereCommand: CommandBase
    {
        private int _x = 0;
        private int _y = 0;
        /// <summary>
        /// Creates a new instance of the AddSphereCommand
        /// </summary>
        /// <param name="sceneContent"></param>
        public AddSphereCommand(SceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected override void Execute()
        {
            var item = new Sphere();
            _x -= 1;
            _y += 1;
            item.Transformation.TranslateX += _x;
            item.Transformation.TranslateY += _y;
            SceneContent.SceneEngine.CommonSceneContainer.Add(item);
        }
    }
}