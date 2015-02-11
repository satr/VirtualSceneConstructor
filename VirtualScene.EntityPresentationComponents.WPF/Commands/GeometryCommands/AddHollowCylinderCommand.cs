using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command creates a <see cref="HollowCylinderEntity" /> and adds it to the scene
    /// </summary>
    public class AddHollowCylinderCommand : AddSceneObjectCommandBase
    {
        private int _x;
        private int _y;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddHollowCylinderCommand"/>
        /// </summary>
        /// <param name="sceneContent">The content of the scene.</param>
        public AddHollowCylinderCommand(ISceneContent sceneContent)
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
            ServiceLocator.Get<SceneContentBusinessManager>().AddSceneElementInSpace<HollowCylinderEntity>(SceneContent, _x, _y, 0, Resources.Title_Hollow_Cylinder);
        }
    }
}