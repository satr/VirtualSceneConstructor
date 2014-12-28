using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="Cylinder" /> geometry.
    /// </summary>
    public class CylinderEntity : SceneEntity
    {
        private Cylinder _cylinder;
        private readonly string _description;
        private float _topRadius;
        private float _baseRadius;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="CylinderEntity" />
        /// </summary>
        public CylinderEntity()
        {
            _description = Resources.Title_Cylinder;
        }

        /// <summary>
        /// The top radius of the cylinder.
        /// </summary>
        public float TopRadius
        {
            get { return _topRadius; }
            set { Common.Helpers.Math.AssignValue(ref _topRadius, value, _cylinder, v => _cylinder.TopRadius = v, 0); }
        }

        /// <summary>
        /// The base radius of the cylinder.
        /// </summary>
        public float BaseRadius
        {
            get { return _baseRadius; }
            set { Common.Helpers.Math.AssignValue(ref _baseRadius, value, _cylinder, v => _cylinder.BaseRadius = v, 0); }
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { Common.Helpers.Math.AssignValue(ref _height, value, _cylinder, v => _cylinder.Height = v, 0); }
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
                base.Geometry = _cylinder = value as Cylinder;
                UpdateFields(_cylinder);
            }
        }

        private void UpdateFields(Cylinder cylinder)
        {
            _topRadius = (float) (cylinder == null ? 0 : cylinder.TopRadius);
            _baseRadius = (float) (cylinder == null ? 0 : cylinder.BaseRadius);
            _height = (float) (cylinder == null ? 0 : cylinder.Height);
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
        /// <returns>Returns the <see cref="Cylinder" /> as <see cref="SceneElement" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            _cylinder = new Cylinder {TopRadius = 1, BaseRadius = 1, Height = 1};
            UpdateFields(_cylinder);
            return _cylinder;
        }
    }
}