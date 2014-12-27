using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    ///  The factory creating a geometry.
    /// </summary>
    public abstract class GeometryFactoryBase
    {
        /// <summary>
        /// Add a face between the point in position x, y, z=0, the point in the position x, y, offsetZ and positions of two last vertices.
        /// </summary>
        /// <param name="x">Coordinate X.</param>
        /// <param name="y">Coordinate Y.</param>
        /// <param name="offsetZ">The offset od coordinate Z where the second point is located to form the edge.</param>
        /// <param name="polygon">The poligone where the face is to be added.</param>
        protected static void AddFace(float x, float y, float offsetZ, Polygon polygon)
        {
            var verticesCount = AddVertices(x, y, offsetZ, polygon);
            if(verticesCount <= 2)
                return;
            var face = new Face();
            face.Indices.Add(new Index(verticesCount - 3, 0));
            face.Indices.Add(new Index(verticesCount - 4, 1));
            face.Indices.Add(new Index(verticesCount - 2, 2));
            face.Indices.Add(new Index(verticesCount - 1, 3));
            polygon.Faces.Add(face);
        }

        private static int AddVertices(float x, float y, float offsetZ, Polygon polygon)
        {
            polygon.Vertices.Add(new Vertex(x, y, offsetZ));
            polygon.Vertices.Add(new Vertex(x, y, 0));
            return polygon.Vertices.Count;
        }

        /// <summary>
        /// The position in 2D plane.
        /// </summary>
        protected class Pos2D
        {
            /// <summary>
            /// Coordinate X.
            /// </summary>
            public float X { get; private set; }
            
            /// <summary>
            /// Coordinate Y
            /// </summary>
            public float Y { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Pos2D"/>
            /// </summary>
            /// <param name="x">Coordinate X.</param>
            /// <param name="y">Coordinate Y.</param>
            public Pos2D(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        /// <summary>
        /// Add a texture coordinated used on faces.
        /// </summary>
        /// <param name="polygon">The poligoone where the texture coordinate are to be added.</param>
        protected static void AddTextureCoordinates(Polygon polygon)
        {
            polygon.UVs.Add(new UV(0, 0));
            polygon.UVs.Add(new UV(0, 1));
            polygon.UVs.Add(new UV(1, 1));
            polygon.UVs.Add(new UV(1, 0));
        }
    }
}