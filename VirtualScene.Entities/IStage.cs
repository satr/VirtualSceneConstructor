using System;
using System.Collections.ObjectModel;

namespace VirtualScene.Entities
{
    /// <summary>
    /// The stage with objects in 3D environment
    /// </summary>
    public interface IStage
    {
        /// <summary>
        /// The name of the stage
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The collection of <see cref="ISceneEntity" /> exists in the scene.
        /// </summary>
        ReadOnlyObservableCollection<ISceneEntity> Items { get; }

        /// <summary>
        /// Add <see cref="ISceneEntity" /> to the stage.
        /// </summary>
        /// <param name="sceneEntity">The <see cref="ISceneEntity" /> to be added.</param>
        void Add(ISceneEntity sceneEntity);

        /// <summary>
        /// Remove <see cref="ISceneEntity" /> from the stage.
        /// </summary>
        /// <param name="sceneEntity">The <see cref="ISceneEntity" /> to be removed.</param>
        void Remove(ISceneEntity sceneEntity);

        /// <summary>
        /// Occures when <see cref="ISceneEntity" /> is added to the stage.
        /// </summary>
        event EventHandler<ISceneEntity> SceneEntityAdded;

        /// <summary>
        /// Occures when <see cref="ISceneEntity" /> is removed from the stage.
        /// </summary>
        event EventHandler<ISceneEntity> SceneEntityRemoved;
    }
}