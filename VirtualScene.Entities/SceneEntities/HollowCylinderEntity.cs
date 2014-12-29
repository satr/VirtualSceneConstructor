using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Common.Helpers;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="HollowCylinder" /> geometry.
    /// </summary>
    public class HollowCylinderEntity : SceneEntity<HollowCylinder>
    {
        private float _radius;
        private float _segmentAngle;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="HollowCylinderEntity" />
        /// </summary>
        public HollowCylinderEntity()
            : base(Resources.Title_Hollow_Cylinder)
        {
        }

        /// <summary>
        /// The radius of the cylinder.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { Math.AssignValue(ref _radius, value, SceneElement, v => SceneElement.Radius = v, 0);}
        }

        /// <summary>
        /// The segment angle of the cylinder.
        /// </summary>
        public float SegmentAngle
        {
            get { return _segmentAngle; }
            set { Math.AssignValue(ref _segmentAngle, value, SceneElement, v => SceneElement.SegmentAngle = v, 0);}
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { Math.AssignValue(ref _height, value, SceneElement, v => SceneElement.Height = v);}
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="HollowCylinder" />.</param>
        protected override void UpdateFields(HollowCylinder sceneElement)
        {
            _radius = sceneElement == null ? 0 : sceneElement.Radius;
            _height = sceneElement == null ? 0 : sceneElement.Height;
            _segmentAngle = sceneElement == null ? 0 : sceneElement.SegmentAngle;
        }

        /// <summary>
        /// Build the <see cref="Cylinder" />.
        /// </summary>
        /// <returns>Returns the <see cref="HollowCylinder" /> as <see cref="SceneElement" />.</returns>
        protected override HollowCylinder CreateGeometry()
        {
            return HollowCylinder.Create(1, 1, 20);
        }
    }
}