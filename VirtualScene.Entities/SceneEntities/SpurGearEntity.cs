using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.Factories;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with <see cref="SpurGear" /> geometry.
    /// </summary>
    public class SpurGearEntity : SceneEntity
    {
        private readonly string _description;
        private float _pitchDiameter;
        private SpurGear _spurGear;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearEntity" />
        /// </summary>
        public SpurGearEntity()
        {
            _description = Resources.Title_Spur_gear;
        }

        /// <summary>
        /// The pitch-diameter of the spur gear.
        /// </summary>
        public float PitchDiameter
        {
            get { return _pitchDiameter; }
            set { Common.Helpers.Math.AssignValue(ref _pitchDiameter, value, _spurGear, v => _spurGear.PitchDiameter = v, 0); }
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { Common.Helpers.Math.AssignValue(ref _height, value, _spurGear, v => _spurGear.Height = v, 0); }
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
                base.Geometry = _spurGear = value as SpurGear;
                UpdateFields(_spurGear);
            }
        }

        private void UpdateFields(SpurGear spurGear)
        {
            _pitchDiameter = spurGear == null? 0: spurGear.PitchDiameter;
            _height = spurGear == null? 0: spurGear.Height;
        }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Build the spur gear shaped <see cref="Polygon" />.
        /// </summary>
        /// <returns>Returns the spur gear shaped <see cref="Polygon" /> as <see cref="SceneElement" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            _spurGear = SpurGear.Create(4f, 0.5f);
            SpurGearBuilder.Build(4f, 0.5f, _spurGear);
            UpdateFields(_spurGear);
            return _spurGear;
        }
    }
}