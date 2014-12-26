using System.Collections.Generic;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Entities;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.SceneEntityCommands
{
    /// <summary>
    /// The command processing the list of the selected <see cref="ISceneEntity" />
    /// </summary>
    public class SelectSceneEntitiesCommand: SelectItemsCommandBase<ISceneEntity>
    {
        private readonly ISceneContent _sceneContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectSceneEntitiesCommand" />
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public SelectSceneEntitiesCommand(ISceneContent sceneContent)
        {
            _sceneContent = sceneContent;
        }

        /// <summary>
        /// Process the collection of <see cref="ISceneEntity" /> passed as a parameter of execution.
        /// </summary>
        /// <param name="items">The collection of items</param>
        protected override void ProcessItems(IEnumerable<ISceneEntity> items)
        {
            _sceneContent.SetSelectedItems(items);
        }
    }
}