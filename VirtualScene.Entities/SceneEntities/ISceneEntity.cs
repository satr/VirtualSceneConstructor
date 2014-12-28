using System;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// Represents an objects in the scene
    /// </summary>
    public interface ISceneEntity
    {
        /// <summary>
        /// The is of the <see cref="ISceneEntity" />
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Visual representation of the <see cref="ISceneEntity" />
        /// </summary>
        SceneElement Geometry { get; set; }

        /// <summary>
        /// The name of the  <see cref="ISceneEntity" /> in the scene
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        string Description { get; }
    }
}