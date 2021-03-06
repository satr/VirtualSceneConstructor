﻿using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.Entities.SceneEntities.Factories;
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
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            _x += 1;
            _y += 1;
            //The class Polygon is used instead of the class Cube - read the comment in the Build factory method.
            ServiceLocator.Get<SceneContentBusinessManager>().AddSceneElementInSpace<CubeEntity>(SceneContent, _x, _y, 0, Resources.Title_Cube);
        }
    }
}
