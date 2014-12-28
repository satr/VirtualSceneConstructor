using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a custom geometry.
    /// </summary>
    public class CustomEntity : SceneEntity<Polygon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomEntity" />
        /// </summary>
        public CustomEntity()
            : base(Resources.Title_Custom_Polygon)
        {
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="T" />.</param>
        protected override void UpdateFields(Polygon sceneElement)
        {
        }

        /// <summary>
        /// Build the <see cref="Polygon" /> specific for the <see cref="CustomEntity" />.
        /// </summary>
        /// <returns>Returns <see cref="Polygon" />.</returns>
        protected override Polygon CreateGeometry()
        {
            return new Polygon();
        }
    }
}