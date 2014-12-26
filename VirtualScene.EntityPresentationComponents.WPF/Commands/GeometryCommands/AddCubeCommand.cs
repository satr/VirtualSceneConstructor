using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Common;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command creates a cube and adds it to the scene
    /// </summary>
    public class AddCubeCommand : AddSceneObjectCommandBase
    {
        private int _x;
        private int _y;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCubeCommand" />
        /// </summary>
        /// <param name="sceneContent">The content of the scene.</param>
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
            //The class Polygon is used instead of the class Cube - read the comment in the CreateCube factory method.
            ServiceLocator.Get<BusinessManager>().AddSceneElementInSpace(SceneContent, GeometryPrimitiveFactory.CreateCube(),  _x, _y, 0, Resources.Title_Cube);
        }
    }
}
