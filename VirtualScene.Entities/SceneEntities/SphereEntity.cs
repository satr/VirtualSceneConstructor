using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="Sphere" /> geometry.
    /// </summary>
    public class SphereEntity : SceneEntity
    {
        private Sphere _sphere;
        private readonly string _description;
        private float _radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="SphereEntity" />
        /// </summary>
        public SphereEntity()
        {
            _description = Resources.Title_Sphere;
        }

        /// <summary>
        /// The radius of the <see cref="Sphere" />
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { Common.Helpers.Math.AssignValue(ref _radius, value, _sphere, v => _sphere.Radius = v, 0); }
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
                if(Geometry == value)
                    return;
                base.Geometry = _sphere = value as Sphere;
                UpdateFields(_sphere);
            }
        }

        private void UpdateFields(Sphere sphere)
        {
            _radius = (float) (sphere == null ? 0 : sphere.Radius);
        }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Build the <see cref="Sphere" />.
        /// </summary>
        /// <returns>Returns the <see cref="Sphere" /> as <see cref="SceneElement" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            const double radius = 1;
            _sphere = new Sphere{Radius = radius};
            UpdateFields(_sphere);
            return _sphere;
        }
    }
}