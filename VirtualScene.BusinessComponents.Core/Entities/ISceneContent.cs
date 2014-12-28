using System;
using System.Collections.Generic;
using VirtualScene.BusinessComponents.Core.Controllers;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// Interface to the content of the scene
    /// </summary>
    public interface ISceneContent
    {
        /// <summary>
        /// The instance of the SceneEngine
        /// </summary>
        ISceneEngine SceneEngine { get; }

        /// <summary>
        /// The stage of the 3D environment
        /// </summary>
        IStage Stage { get; set; }

        /// <summary>
        /// Navigation in the scene
        /// </summary>
        ISceneNavigator Navigator { get; set; }

        /// <summary>
        /// The collection of items selected in the scene.
        /// </summary>
        IEnumerable<ISceneEntity> SelectedItems { get; }

        /// <summary>
        /// Occures when the <see cref="ISceneContent.Stage" /> is changed.
        /// </summary>
        event EventHandler<IStage> StageChanged;

        /// <summary>
        /// Set the collection of selected items.
        /// </summary>
        /// <param name="items">The collection of <see cref="ISceneEntity" /></param>
        void SetSelectedItems(IEnumerable<ISceneEntity> items);

        /// <summary>
        /// Subscribe on the action when items of specified type are selected.
        /// </summary>
        /// <param name="subscriber">The subcriber to be notified about the operation.</param>
        /// <typeparam name="T">The type of selected items.</typeparam>
        void SubscribeOnSelectedItems<T>(ICollectionItemsOperationSubscriber subscriber);

        /// <summary>
        /// Occures when the <seealso cref="SceneContent.Stage" /> is changed.
        /// </summary>
        event EventHandler<IEnumerable<ISceneEntity>> SelectedSceneElementsChanged;
    }
}