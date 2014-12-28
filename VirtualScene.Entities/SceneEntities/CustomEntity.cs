using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a custom geometry.
    /// </summary>
    public class CustomEntity : SceneEntity
    {
        private readonly string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomEntity" />
        /// </summary>
        public CustomEntity()
        {
            _description = Resources.Title_Custom_Polygon;
        }

        /// <summary>
        /// Build the <see cref="Polygon" /> specific for the <see cref="CustomEntity" />.
        /// </summary>
        /// <returns>Returns <see cref="Polygon" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            return new Polygon();
        }

        /// <summary>
        /// The description of the <see cref="CustomEntity" />
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }
    }
}