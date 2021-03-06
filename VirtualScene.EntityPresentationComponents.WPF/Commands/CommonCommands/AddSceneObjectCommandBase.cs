﻿using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.CommonCommands
{
    /// <summary>
    /// Adds a new scene element to a scene
    /// </summary>
    public abstract class AddSceneObjectCommandBase : CommandWithSceneContentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSceneObjectCommandBase" />
        /// </summary>
        /// <param name="sceneContent"></param>
        protected AddSceneObjectCommandBase(ISceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// The SceneEngine
        /// </summary>
        protected ISceneEngine SceneEngine
        {
            get { return SceneContent.SceneEngine; }
        }
    }
}