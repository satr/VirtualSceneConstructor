using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command creates a cylinder and adds it to the scene
    /// </summary>
    public class AddCylinderCommand : AddSceneObjectCommandBase
    {
        private int _x;
        private int _y;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCylinderCommand"/>
        /// </summary>
        /// <param name="sceneContent">The content of the scene.</param>
        public AddCylinderCommand(ISceneContent sceneContent)
            : base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            _x += 1;
            _y += 1;
            ServiceLocator.Get<SceneContentBusinessManager>().AddSceneElementInSpace(SceneContent, CylinderFactory.Create(), _x, _y, 0, Resources.Title_Cylinder);
        }
    }
}
