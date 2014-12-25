using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Common;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command creates a gear and adds it to the scene
    /// </summary>
    public class AddGearCommand : AddSceneObjectCommandBase
    {
        private int _x;
        private int _y;
        /// <summary>
        /// Creates a new instance of the <see cref="AddGearCommand"/>
        /// </summary>
        /// <param name="sceneContent">The content of the scene.</param>
        public AddGearCommand(ISceneContent sceneContent)
            : base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected override void Execute()
        {
            _x += 1;
            _y += 1;
            //TODO: temporary use cylinder
            ServiceLocator.Get<BusinessManager>().AddSceneElementInSpace(SceneContent, CylinderFactory.Create(), _x, _y, 0, Resources.Title_Gear);
        }
    }
}
