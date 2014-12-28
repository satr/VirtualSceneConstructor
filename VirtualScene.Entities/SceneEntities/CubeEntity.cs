using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.Factories;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a cubic <see cref="Polygon" /> geometry.
    /// The class <see cref="Polygon" /> is used instead of the class <see cref="Cube" /> 
    /// because the <see cref="Cube" /> invokes in its default constructor the method populating its collections,
    /// because of this collections UVs, Vertices and Faces are populated second time during deserialization.
    /// </summary>
    public class CubeEntity : SceneEntity
    {
        private readonly string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="CubeEntity" />
        /// </summary>
        public CubeEntity()
        {
            _description = Resources.Title_Cube;
        }

        /// <summary>
        /// Build the cubic <see cref="Polygon" />.
        /// </summary>
        /// <returns>Returns cubic <see cref="Polygon" /> as <see cref="SceneElement" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            return CubeFactory.Create(1);
        }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }
    }
}