using System;
using System.Collections.Generic;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities.Factories
{
    /// <summary>
    /// The builder makes the hollow cylinder geometry.
    /// </summary>
    public class HollowCylinderBuilder : GeometryFactoryBase
    {
        const double Rad2DegRatio = Math.PI / 180;

        /// <summary>
        /// Build a new <see cref="HollowCylinder" />.
        /// </summary>
        /// <param name="radius">The radius of the cylinder.</param>
        /// <param name="height">The height of the cylinder.</param>
        /// <param name="segmentAngle">The angle of segments of the cylinder.</param>
        /// <param name="hollowCylinder"></param>
        public static void Build(float radius, float height, float segmentAngle, HollowCylinder hollowCylinder)
        {
            if (radius <= float.Epsilon || segmentAngle <= float.Epsilon)
                return;

            if(hollowCylinder.UVs.Count == 0)
                AddTextureCoordinates(hollowCylinder);

            const float centerX = 0;
            const float centerY = 0;
            foreach (var pos in GetPositionsForCircle(radius, centerX, centerY, segmentAngle))
                AddFace(pos.X, pos.Y, height, hollowCylinder);
        }

        private static IEnumerable<Pos2D> GetPositionsForCircle(float radius, float centerX, float centerY, float segmentAngle)
        {
            yield return new Pos2D(0 + centerX, radius + centerY);
            for (var angle = segmentAngle; angle < 360 + segmentAngle; angle += segmentAngle)
            {
                var rad = angle * Rad2DegRatio;
                yield return new Pos2D((float) (radius * Math.Sin(rad)) + centerX, (float) (radius * Math.Cos(rad)) + centerY);
            }
        }
    }
}
