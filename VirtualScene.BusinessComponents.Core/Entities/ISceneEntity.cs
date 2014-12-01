using SharpGL.SceneGraph.Core;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// Represents an objects in the scene
    /// </summary>
    public interface ISceneEntity
    {
        /// <summary>
        /// Visual representation of the SceneEntity
        /// </summary>
        SceneElement Geometry { get; set; }

        /// <summary>
        /// The name of the entity in the scene
        /// </summary>
        string Name { get; set; }
    }
}