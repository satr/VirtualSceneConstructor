using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.Entities.SceneEntities.Builders
{
    /// <summary>
    ///  The builder making geometry.
    /// </summary>
    public abstract class SceneElementBuilderBase
    {
        /// <summary>
        /// Add a face between the point in position x, y, z=0, the point in the position x, y, offsetZ and positions of two last vertices.
        /// </summary>
        /// <param name="x">Coordinate X.</param>
        /// <param name="y">Coordinate Y.</param>
        /// <param name="offsetZ">The offset od coordinate Z where the second point is located to form the edge.</param>
        /// <param name="polygon">The poligone where the face is to be added.</param>
        /// <param name="addendum"></param>
        /// <param name="initVerticesCount"></param>
        /// <param name="material"></param>
        protected static VerticesPair AddFace(float x, float y, float offsetZ, Polygon polygon, float addendum, int initVerticesCount, Material material = null)
        {
            var pair = new VerticesPair(x, y, offsetZ, addendum);
            polygon.Vertices.Add(pair.Top);
            polygon.Vertices.Add(pair.Base);

            var verticesCount = polygon.Vertices.Count;
            if (verticesCount - initVerticesCount <= 2)
                return pair;
            AddTriangleFace(polygon, material, verticesCount, 3, 2, 1);
            AddTriangleFace(polygon, material, verticesCount, 3, 2, 4);
            return pair;
        }

        protected static void AddTriangleFace(Polygon polygon, Material material, int verticesCount, params int[] verticeOffsets)
        {
            var face = new Face();
            if (material != null)
                face.Material = material;
            foreach (var verticeOffset in verticeOffsets)
                face.Indices.Add(new Index(verticesCount - verticeOffset, 0));
            polygon.Faces.Add(face);
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