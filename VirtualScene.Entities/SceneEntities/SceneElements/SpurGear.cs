using System;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.Entities.SceneEntities.SceneElements
{
    /// <summary>
    /// The spur gear polygon.
    /// </summary>
    [Serializable]
    public class SpurGear : Polygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGear" />. 
        /// Only for deserialisation - use method <see cref="Create" /> to create a new instance.
        /// </summary>
        private SpurGear()
        {
        }

        /// <summary>
        /// The face width.
        /// </summary>
        public float FaceWidth { get; set; }

        /// <summary>
        /// The pressure angle of teeth.
        /// </summary>
        public float PressureAngle { get; set; }

        /// <summary>
        /// The number of teeth.
        /// </summary>
        public int NumberOfTeeth { get; set; }

        /// <summary>
        /// The outside diameter.
        /// </summary>
        public float OutsideDiameter { get; set; }

        /// <summary>
        /// The pitch diameter.
        /// </summary>
        public float PitchDiameter { get; set; }

        /// <summary>
        /// The addendum.
        /// </summary>
        [XmlIgnore]
        public float Addendum { get; set;}

        /// <summary>
        /// The dedendum.
        /// </summary>
        [XmlIgnore]
        public float Dedendum { get; set;}

        /// <summary>
        /// The whole depth.
        /// </summary>
        [XmlIgnore]
        public float WholeDepth { get; set; }

        /// <summary>
        /// The working depth.
        /// </summary>
        [XmlIgnore]
        public float WorkingDepth { get; set; }

        /// <summary>
        /// The diametral pitch.
        /// </summary>
        [XmlIgnore]
        public float DiametralPitch { get; set; }

        /// <summary>
        /// The tooth thicknes at the pitch diameter.
        /// </summary>
        [XmlIgnore]
        public float ToothThickness { get; set; }

        /// <summary>
        /// The circular pitch - step of teeth at the pitch diameter.
        /// </summary>
        [XmlIgnore]
        public float CircularPitch { get; set; }
        
        /// <summary>
        /// The diameter of the shaft.
        /// </summary>
        public float ShaftDiameter { get; set; }

        /// <summary>
        /// Create a new instance of the <see cref="SpurGear" />.
        /// </summary>
        /// <param name="pitchDiameter">The pitch diameter.</param>
        /// <param name="outsideDiameter">The outside diameter.</param>
        /// <param name="faceWidth">The face width.</param>
        /// <param name="numberOfTeeth">The number of teeth.</param>
        /// <param name="shaftDiameter">The diameter of the shaft.</param>
        /// <param name="pressureAngle">The pressure angle of teeth.</param>
        /// <returns>Returns the new instance of <see cref="SpurGear" />.</returns>
        public static SpurGear Create(float pitchDiameter, float outsideDiameter, float faceWidth, int numberOfTeeth, float shaftDiameter, float pressureAngle)
        {
            var spurGear = new SpurGear
            {
                FaceWidth = faceWidth,
                PressureAngle = pressureAngle,
                NumberOfTeeth = numberOfTeeth,
                PitchDiameter = pitchDiameter,
                OutsideDiameter = outsideDiameter,
                ShaftDiameter = shaftDiameter,
            };
            return spurGear;
        }
    }
}