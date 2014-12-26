using System;
using System.Collections.Generic;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// The factory creating gear geometry.
    /// </summary>
    public class GearFactory : GeometryFactoryBase
    {
        /// <summary>
        /// Create a new gear.
        /// </summary>
        /// <returns></returns>
        public static Polygon Create()
        {

            var polygon = new Polygon();

            AddTextureCoordinates(polygon);

            const int radius = 5;
            const int heightZ = 1;
            const float centerX = 2;
            const float centerY = -1;
            const int segmentAngle = 5;

            foreach (var pos in GetPositionsForGear(radius, centerX, centerY, segmentAngle))
                AddFace(pos.X, pos.Y, heightZ, polygon);

            return polygon;
        }

        private static IEnumerable<Pos2D> GetPositionsForGear(int radius, float centerX, float centerY, int segmentAngle)
        {
            yield return new Pos2D(0 + centerX, radius + centerY);
            const double rad2DegRatio = Math.PI / 180;
            for (var angle = segmentAngle; angle < 360 + segmentAngle; angle += segmentAngle)
            {
                var rad = angle*rad2DegRatio;
                yield return new Pos2D((float) (radius*Math.Sin(rad)) + centerX, (float) (radius*Math.Cos(rad)) + centerY);
            }
        }
    }
}