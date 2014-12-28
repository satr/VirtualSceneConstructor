using System;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="HollowCylinder" /> geometry.
    /// </summary>
    public class HollowCylinderEntity : SceneEntity
    {
        private HollowCylinder _hollowCylinder;
        private readonly string _description;
        private float _radius;
        private float _segmentAngle;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="HollowCylinderEntity" />
        /// </summary>
        public HollowCylinderEntity()
        {
            _description = Resources.Title_Hollow_Cylinder;
        }

        /// <summary>
        /// The radius of the cylinder.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { Common.Helpers.Math.AssignValue(ref _radius, value, _hollowCylinder, v => _hollowCylinder.Radius = v, 0);}
        }

        /// <summary>
        /// The segment angle of the cylinder.
        /// </summary>
        public float SegmentAngle
        {
            get { return _segmentAngle; }
            set { Common.Helpers.Math.AssignValue(ref _segmentAngle, value, _hollowCylinder, v => _hollowCylinder.SegmentAngle = v, 0);}
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { Common.Helpers.Math.AssignValue(ref _height, value, _hollowCylinder, v => _hollowCylinder.Height = v);}
        }

        /// <summary>
        /// Representation of the <see cref="ISceneEntity" /> in the scene.
        /// </summary>
        [XmlIgnore]
        public override SceneElement Geometry
        {
            get { return base.Geometry; }
            set
            {
                if (Geometry == value)
                    return;
                base.Geometry = _hollowCylinder = value as HollowCylinder;
                UpdateFields(_hollowCylinder);
            }
        }

        private void UpdateFields(HollowCylinder hollowCylinder)
        {
            _radius = hollowCylinder == null ? 0 : hollowCylinder.Radius;
            _height = hollowCylinder == null ? 0 : hollowCylinder.Height;
            _segmentAngle = hollowCylinder == null ? 0 : hollowCylinder.SegmentAngle;
        }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Build the <see cref="Cylinder" />.
        /// </summary>
        /// <returns>Returns the <see cref="HollowCylinder" /> as <see cref="SceneElement" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            _hollowCylinder = HollowCylinder.Create(1, 1, 20);
            UpdateFields(_hollowCylinder);
            return _hollowCylinder;
        }
    }
}