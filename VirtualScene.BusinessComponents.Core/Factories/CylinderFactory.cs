using System;
using System.Collections.Generic;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// The factory creating gear geometry.
    /// </summary>
    public class CylinderFactory
    {
        /// <summary>
        /// Create a new gear.
        /// </summary>
        /// <returns></returns>
        public static Polygon Create()
        {

            var polygon = new Polygon();

            polygon.UVs.Add(new UV(0, 0));
            polygon.UVs.Add(new UV(0, 1));
            polygon.UVs.Add(new UV(1, 1));
            polygon.UVs.Add(new UV(1, 0));

            const int radius = 5;
            const int heightZ = 1;
            const float centerX = 2;
            const float centerY = -1;
            const int segmentAngle = 5;

            foreach (var pos in GetPositionsForCircle(radius, centerX, centerY, segmentAngle))
                AddFace(polygon, heightZ, pos.X, pos.Y);

            return polygon;
        }

        private static IEnumerable<Pos> GetPositionsForCircle(int radius, float centerX, float centerY, int segmentAngle)
        {
            yield return new Pos(0 + centerX, radius + centerY);
            const double rad2DegRatio = Math.PI / 180;
            for (var angle = segmentAngle; angle < 360 + segmentAngle; angle += segmentAngle)
            {
                var rad = angle*rad2DegRatio;
                yield return new Pos((float) (radius*Math.Sin(rad)) + centerX, (float) (radius*Math.Cos(rad)) + centerY);
            }
        }

        private static void AddFace(Polygon polygon, int height, float x, float y)
        {
            var verticesCount = AddVertices(x, y, height, polygon);
            if(verticesCount <= 2)
                return;
            var face = new Face();
            face.Indices.Add(new Index(verticesCount - 3, 0));
            face.Indices.Add(new Index(verticesCount - 4, 1));
            face.Indices.Add(new Index(verticesCount - 2, 2));
            face.Indices.Add(new Index(verticesCount - 1, 3));
            polygon.Faces.Add(face);
        }

        private static int AddVertices(float x, float y, int height, Polygon polygon)
        {
            polygon.Vertices.Add(new Vertex(x, y, height));
            polygon.Vertices.Add(new Vertex(x, y, 0));
            return polygon.Vertices.Count;
        }

        private class Pos
        {
            public float X { get; private set; }
            public float Y { get; private set; }

            public Pos(float x, float y)
            {
                X = x;
                Y = y;
            }
        }
    }

}
