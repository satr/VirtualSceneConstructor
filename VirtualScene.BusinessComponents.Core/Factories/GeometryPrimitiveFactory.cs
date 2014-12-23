using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// The factory creating geometry primitives.
    /// </summary>
    public class GeometryPrimitiveFactory
    {

        /// <summary>
        /// Create a new cube.
        /// The class Polygon is used instead of the class Cube because the Cube invokes the method populating its collections in the default constructor.
        /// This leads to collections populated second time during deserialization.
        /// </summary>
        /// <returns>The cube with populated geometry.</returns>
        public static Polygon CreateCube()
        {
            var cube = new Polygon();
            cube.UVs.Add(new UV(0, 0));
            cube.UVs.Add(new UV(0, 1));
            cube.UVs.Add(new UV(1, 1));
            cube.UVs.Add(new UV(1, 0));
	
			//	Add the vertices.
			cube.Vertices.Add(new Vertex(-1, -1, -1));
			cube.Vertices.Add(new Vertex( 1, -1, -1));
			cube.Vertices.Add(new Vertex( 1, -1,  1));
			cube.Vertices.Add(new Vertex(-1, -1,  1));
			cube.Vertices.Add(new Vertex(-1,  1, -1));
			cube.Vertices.Add(new Vertex( 1,  1, -1));
			cube.Vertices.Add(new Vertex( 1,  1,  1));
			cube.Vertices.Add(new Vertex(-1,  1,  1));

			//	Add the faces.
			var face = new Face();	//	bottom
			face.Indices.Add(new Index(1, 0));
			face.Indices.Add(new Index(2, 1));
			face.Indices.Add(new Index(3, 2));
			face.Indices.Add(new Index(0, 3));
            cube.Faces.Add(face);

			face = new Face();		//	top
			face.Indices.Add(new Index(7, 0));
			face.Indices.Add(new Index(6, 1));
			face.Indices.Add(new Index(5, 2));
			face.Indices.Add(new Index(4, 3));
            cube.Faces.Add(face);

			face = new Face();		//	right
			face.Indices.Add(new Index(5, 0));
			face.Indices.Add(new Index(6, 1));
			face.Indices.Add(new Index(2, 2));
			face.Indices.Add(new Index(1, 3));
            cube.Faces.Add(face);
	
			face = new Face();		//	left
			face.Indices.Add(new Index(7, 0));
			face.Indices.Add(new Index(4, 1));
			face.Indices.Add(new Index(0, 2));
			face.Indices.Add(new Index(3, 3));
            cube.Faces.Add(face);

			face = new Face();		// front
			face.Indices.Add(new Index(4, 0));
			face.Indices.Add(new Index(5, 1));
			face.Indices.Add(new Index(1, 2));
			face.Indices.Add(new Index(0, 3));
            cube.Faces.Add(face);

			face = new Face();		//	back
			face.Indices.Add(new Index(6, 0));
			face.Indices.Add(new Index(7, 1));
			face.Indices.Add(new Index(3, 2));
			face.Indices.Add(new Index(2, 3));
            cube.Faces.Add(face);
            return cube;
        }
    }
}
