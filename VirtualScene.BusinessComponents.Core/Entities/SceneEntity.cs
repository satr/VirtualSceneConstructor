using SharpGL.SceneGraph.Core;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// Represents an objects in the scene
    /// </summary>
    public class SceneEntity : ISceneEntity
    {
        /// <summary>
        /// Visual representation of the SceneEntity
        /// </summary>
        public SceneElement Geometry { get; set; }
    }
}