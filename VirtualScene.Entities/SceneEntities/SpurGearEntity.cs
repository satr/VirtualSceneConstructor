using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Common.Helpers;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.Factories;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with <see cref="SpurGear" /> geometry.
    /// </summary>
    public class SpurGearEntity : SceneEntity<SpurGear>
    {
        private float _pitchDiameter;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearEntity" />
        /// </summary>
        public SpurGearEntity()
            : base(Resources.Title_Spur_gear)
        {
        }

        /// <summary>
        /// The pitch-diameter of the spur gear.
        /// </summary>
        public float PitchDiameter
        {
            get { return _pitchDiameter; }
            set { Math.AssignValue(ref _pitchDiameter, value, ConcreteGeometry, v => ConcreteGeometry.PitchDiameter = v, 0); }
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { Math.AssignValue(ref _height, value, ConcreteGeometry, v => ConcreteGeometry.Height = v, 0); }
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="SpurGear" />.</param>
        protected override void UpdateFields(SpurGear sceneElement)
        {
            _pitchDiameter = sceneElement == null? 0: sceneElement.PitchDiameter;
            _height = sceneElement == null? 0: sceneElement.Height;
        }

        /// <summary>
        /// Build the spur gear shaped <see cref="Polygon" />.
        /// </summary>
        /// <returns>Returns the spur gear shaped <see cref="Polygon" /> as <see cref="SceneElement" />.</returns>
        protected override SpurGear CreateGeometry()
        {
            var spurGear = SpurGear.Create(4f, 0.5f);
            SpurGearBuilder.Build(4f, 0.5f, spurGear);
            return spurGear;
        }
    }
}