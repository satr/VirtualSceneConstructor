using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VirtualScene.Entities;

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
        ObservableCollection<ISceneEntity> SelectedItems { get; }

        /// <summary>
        /// Occures when the <see cref="ISceneContent.Stage" /> is changed.
        /// </summary>
        event EventHandler<IStage> StageChanged;

        /// <summary>
        /// Set the collection of selected items.
        /// </summary>
        /// <param name="items">The collection of <see cref="ISceneEntity" /></param>
        void SetSelectedItems(IEnumerable<ISceneEntity> items);
    }
}