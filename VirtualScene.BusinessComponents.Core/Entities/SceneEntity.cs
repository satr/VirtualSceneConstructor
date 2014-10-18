using SharpGL.SceneGraph.Core;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// An entity in the scene
    /// </summary>
    public class SceneEntity : ISceneEntity
    {
        /// <summary>
        /// Visual representation of the entity in the scene
        /// </summary>
        public SceneElement Geometry { get; set; }

        /// <summary>
        /// The name of the entity in the scene
        /// </summary>
        public string Name { get; set; }
    }
}