using System.Collections.Generic;
using System.Linq;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.Entities.SceneEntities.Factories
{
    /// <summary>
    /// Build cubic <see cref="Polygon" />.
    /// </summary>
    public class CubeFactory
    {
        /// <summary>
        /// Build cubic <see cref="Polygon" />.
        /// </summary>
        /// <param name="height"></param>
        /// <returns>The <see cref="Polygon" /> with cubic geometry.</returns>
        public static Polygon Create(float height)
        {
            var hh = height/2;
            var polygon = new Polygon();
            polygon.UVs.AddRange(new[]
            {
                new UV(0, 0),
                new UV(0, 1),
                new UV(1, 1),
                new UV(1, 0)
            });
            polygon.Vertices.AddRange(new[]
            {
                new Vertex(-hh, -hh, -hh),
                new Vertex(hh, -hh, -hh),
                new Vertex(hh, -hh, hh),
                new Vertex(-hh, -hh, hh),
                new Vertex(-hh, hh, -hh),
                new Vertex(hh, hh, -hh),
                new Vertex(hh, hh, hh),
                new Vertex(-hh, hh, hh)
            });
            var face = new Face(); // The bottom face
            face.Indices.AddRange(CreateIndices(1, 2, 3, 0));
            polygon.Faces.Add(face);
            face = new Face(); // The top face
            face.Indices.AddRange(CreateIndices(7, 6, 5, 4));
            polygon.Faces.Add(face);
            face = new Face(); // The right face
            face.Indices.AddRange(CreateIndices(5, 6, 2, 1));
            polygon.Faces.Add(face);
            face = new Face(); // The left face
            face.Indices.AddRange(CreateIndices(7, 4, 0, 3));
            polygon.Faces.Add(face);
            face = new Face(); // The front face
            face.Indices.AddRange(CreateIndices(4, 5, 1, 0));
            polygon.Faces.Add(face);
            face = new Face(); // The back face
            face.Indices.AddRange(CreateIndices(6, 7, 3, 2));
            polygon.Faces.Add(face);
            return polygon;
        }

        private static IEnumerable<Index> CreateIndices(params int[] vertices)
        {
            var uv = 0;
            return vertices.Select(vertex => new Index(vertex, uv++));
        }
    }
}
