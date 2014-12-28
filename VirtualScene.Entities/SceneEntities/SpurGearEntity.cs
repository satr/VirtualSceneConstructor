using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.Factories;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a spur gear shaped <see cref="Polygon" /> geometry.
    /// </summary>
    public class SpurGearEntity : SceneEntity
    {
        private readonly string _description;

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
        public float PitchDiameter { get; set; }

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
            return GearFactory.Create();
        }
    }
}