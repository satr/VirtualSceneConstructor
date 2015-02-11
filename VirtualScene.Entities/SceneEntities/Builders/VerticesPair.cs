using SharpGL.SceneGraph;

namespace VirtualScene.Entities.SceneEntities.Builders
{
    /// <summary>
    /// The pair of vertices to build solid body.
    /// </summary>
    public class VerticesPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticesPair" />
        /// </summary>
        /// <param name="x">The X coordinate of the <see cref="Vertex" />.</param>
        /// <param name="y">The Y coordinate of the <see cref="Vertex" />.</param>
        /// <param name="offsetZ">The Z coordinate offset of the top <see cref="Vertex" />. The base <see cref="Vertex" /> has the offset "0".</param>
        /// <param name="addendum">The addendum to the <see cref="offsetZ" /> - to extend the axiliary geometry out of the solid body.</param>
        public VerticesPair(float x, float y, float offsetZ, float addendum)
        {
            Top = new Vertex(x, y, offsetZ + addendum);
            Base = new Vertex(x, y, 0 - addendum);
        }

        /// <summary>
        /// The top vertex of the solid body.
        /// </summary>
        public Vertex Top { get; set; }

        /// <summary>
        /// The base vertex of the solid body.
        /// </summary>
        public Vertex Base { get; set; }
    }
}