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
    public class CubeEntity : SceneEntity<Polygon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubeEntity" />
        /// </summary>
        public CubeEntity()
            : base(Resources.Title_Cube)
        {
        }

        protected override void UpdateFields(Polygon sceneElement)
        {
        }

        /// <summary>
        /// Build the cubic <see cref="Polygon" />.
        /// </summary>
        /// <returns>Returns cubic <see cref="Polygon" /> as <see cref="SceneElement" />.</returns>
        protected override Polygon CreateGeometry()
        {
            return CubeFactory.Create(1);
        }
    }
}