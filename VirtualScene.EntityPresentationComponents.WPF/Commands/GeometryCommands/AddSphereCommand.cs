using SharpGL.SceneGraph.Quadrics;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command creates a sphere and adds it to the scene
    /// </summary>
    public class AddSphereCommand: AddSceneObjectCommandBase
    {
        private int _x, _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddSphereCommand" />
        /// </summary>
        /// <param name="sceneContent"></param>
        public AddSphereCommand(ISceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            _x -= 1;
            _y += 1;
            ServiceLocator.Get<SceneContentBusinessManager>().AddSceneElementInSpace<SphereEntity>(SceneContent, _x, _y, 0, Resources.Title_Sphere);
        }
    }
}