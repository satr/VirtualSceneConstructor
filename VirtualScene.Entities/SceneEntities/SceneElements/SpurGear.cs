using System;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.SceneEntities.Factories;

namespace VirtualScene.Entities.SceneEntities.SceneElements
{
    /// <summary>
    /// The spur gear polygon.
    /// </summary>
    [Serializable]
    public class SpurGear : Polygon
    {
        private float _height;
        private float _pitchDiameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGear" />. 
        /// Only for deserialisation - use method <see cref="Create" /> to create a new instance.
        /// </summary>
        private SpurGear()
        {
        }

        /// <summary>
        /// The pitch diameter of the spur gear.
        /// </summary>
        public float PitchDiameter
        {
            get { return _pitchDiameter; }
            set
            {
                if (Common.Helpers.Math.AssignValue(ref _pitchDiameter, value, 0))
                    Rebuild();
            }
        }

        /// <summary>
        /// The height of the spur gear.
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
            SpurGearBuilder.Build(PitchDiameter, Height, this);
        }

        /// <summary>
        /// Create a new instance of the <see cref="SpurGear" />.
        /// </summary>
        /// <param name="pitchDiameter">The pitch diameter of the spur gear.</param>
        /// <param name="height">The height of the spur gear.</param>
        /// <returns>Returns the new instance of <see cref="SpurGear" />.</returns>
        public static SpurGear Create(float pitchDiameter, float height)
        {
            var spurGear = new SpurGear { _pitchDiameter = pitchDiameter, _height = height };
            SpurGearBuilder.Build(pitchDiameter, height, spurGear);
            return spurGear;
        }
    }
}