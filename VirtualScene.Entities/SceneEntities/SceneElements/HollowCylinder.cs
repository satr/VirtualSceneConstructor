using System;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.SceneEntities.Builders;
using VirtualScene.Entities.SceneEntities.Factories;

namespace VirtualScene.Entities.SceneEntities.SceneElements
{
    /// <summary>
    /// The hollow cylinder polygon.
    /// </summary>
    [Serializable]
    public class HollowCylinder : Polygon
    {
        private float _radius;
        private float _segmentAngle;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="HollowCylinder" />. 
        /// Only for deserialisation - use method <see cref="Create" /> to create a new instance.
        /// </summary>
        private HollowCylinder()
        {
        }

        /// <summary>
        /// The radius of the cylinder.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set
            {
                if (Common.Helpers.Math.AssignValue(ref _radius, value, 0))
                    Rebuild();
            }
        }

        /// <summary>
        /// The segment angle of the cylinder.
        /// </summary>
        public float SegmentAngle
        {
            get { return _segmentAngle; }
            set
            {
                if (Common.Helpers.Math.AssignValue(ref _segmentAngle, value, 0))
                    Rebuild();
            }
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set
            {
                if (Common.Helpers.Math.AssignValue(ref _height, value))
                    Rebuild();
            }
        }

        private void Rebuild()
        {
            Faces.Clear();
            Vertices.Clear();
            HollowCylinderBuilder.Build(Radius, Height, SegmentAngle, this);
        }

        /// <summary>
        /// Create a new instance of the <see cref="HollowCylinder" />.
        /// </summary>
        /// <param name="radius">The radius of the cylinder.</param>
        /// <param name="height">The height of the cylinder.</param>
        /// <param name="segmentAngle">The angle of each of cegments of the cylinder.</param>
        /// <returns></returns>
        public static HollowCylinder Create(int radius, int height, int segmentAngle)
        {
            var hollowCylinder = new HollowCylinder {_radius = radius, _height = height, _segmentAngle = segmentAngle};
            HollowCylinderBuilder.Build(radius, height, segmentAngle, hollowCylinder);
            return hollowCylinder;
        }
    }
}
