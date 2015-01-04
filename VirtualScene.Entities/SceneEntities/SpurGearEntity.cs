using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.Builders;
using VirtualScene.Entities.SceneEntities.CalculationStrategies;
using VirtualScene.Entities.SceneEntities.SceneElements;
using Math = VirtualScene.Common.Helpers.Math;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with <see cref="SpurGear" /> geometry.
    /// </summary>
    [Serializable]
    [KnownType(typeof(SpurGearCalculationStrategyBase))]
    [KnownType(typeof(SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter))]
    [KnownType(typeof(SpurGearCalculationStrategyByNumberOfTeethAndPitchDiameter))]
    public class SpurGearEntity : SceneEntity<SpurGear>
    {
        private SpurGearCalculationStrategyBase _calculationStrategy;
        private float _faceWidth;
        private float _outsideDiameter;
        private int _numberOfTeeth;
        private float _pitchDiameter;
        private float _shaftDiameter;
        private bool _showAxiliaryGeometry;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearEntity" />
        /// </summary>
        public SpurGearEntity()
            : base(Resources.Title_Spur_gear)
        {
            CalculationStrategy = new SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter();
        }

        /// <summary>
        /// The face width of the cylinder.
        /// </summary>
        [XmlIgnore]
        public float FaceWidth
        {
            get { return _faceWidth; }
            set
            {
                if (Math.AssignValue(ref _faceWidth, value, SceneElement, v => SceneElement.FaceWidth = v, 0))
                    Rebuild();
            }
        }

        /// <summary>
        /// The diameter of the shaft.
        /// </summary>
        [XmlIgnore]
        public float ShaftDiameter
        {
            get { return _shaftDiameter; }
            set
            {
                if (Math.AssignValue(ref _shaftDiameter, value, SceneElement, v => SceneElement.ShaftDiameter = v, 0))
                    Rebuild();
            }
        }

        /// <summary>
        /// The number of teeth.
        /// </summary>
        [XmlIgnore]
        public int NumberOfTeeth
        {
            get { return _numberOfTeeth; }
            set
            {
                if (SceneElement == null || !Math.AssignValue(ref _numberOfTeeth, value, CalculationStrategy.ValidateIsAllowedToChangeNumberOfTeeth, 0)) 
                    return;
                SceneElement.NumberOfTeeth = _numberOfTeeth;
                RecalculateGear();
            }
        }

        /// <summary>
        /// The outside diameter.
        /// </summary>
        [XmlIgnore]
        public float OutsideDiameter
        {
            get { return _outsideDiameter; }
            set
            {
                if (SceneElement == null || !Math.AssignValue(ref _outsideDiameter, value, CalculationStrategy.ValidateIsAllowedToChangeOutsideDiameter, 0))
                    return;
                SceneElement.OutsideDiameter = _outsideDiameter;
                RecalculateGear();
            }
        }

        /// <summary>
        /// The pitch diameter.
        /// </summary>
        [XmlIgnore]
        public float PitchDiameter
        {
            get { return _pitchDiameter; }
            set
            {
                if (SceneElement == null || !Math.AssignValue(ref _pitchDiameter, value, CalculationStrategy.ValidateIsAllowedToChangePitchDiameter, 0))
                    return;
                SceneElement.PitchDiameter = _pitchDiameter;
                RecalculateGear();
            }
        }

        /// <summary>
        /// The strategy to calculate parameters of the spur gear.
        /// <exception cref="CalculationStrategyNullException">When null is assigned.</exception>
        /// </summary>
        public SpurGearCalculationStrategyBase CalculationStrategy
        {
            get { return _calculationStrategy; }
            set
            {
                if(_calculationStrategy == value)
                    return;
                if (value == null)
                    throw new CalculationStrategyNullException();
                _calculationStrategy = value;
                RecalculateGear();
            }
        }

        /// <summary>
        /// Show the axiliary geometry.
        /// </summary>
        [XmlIgnore]
        public bool ShowAxiliaryGeometry
        {
            get { return _showAxiliaryGeometry; }
            set
            {
                if(_showAxiliaryGeometry == value)
                    return;
                _showAxiliaryGeometry = value;
                Rebuild();
            }
        }

        private void RecalculateGear()
        {
            if(SceneElement == null)
                return;
            CalculationStrategy.Calculate(this);
            Rebuild();
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="SpurGear" />.</param>
        protected override void UpdateFields(SpurGear sceneElement)
        {
            _numberOfTeeth = sceneElement == null? 0: sceneElement.NumberOfTeeth;
            _outsideDiameter = sceneElement == null? 0: sceneElement.OutsideDiameter;
            _pitchDiameter = sceneElement == null ? 0 : sceneElement.PitchDiameter;
            _faceWidth = sceneElement == null? 0: sceneElement.FaceWidth;
            _shaftDiameter = sceneElement == null ? 0 : sceneElement.ShaftDiameter;
            RecalculateGear();
        }

        /// <summary>
        /// Build the spur gear shaped <see cref="Polygon" />.
        /// </summary>
        /// <returns>Returns the spur gear shaped <see cref="Polygon" /> as <see cref="SceneElement" />.</returns>
        protected override SpurGear CreateGeometry()
        {
            return CalculationStrategy.CreateSpurGear();
        }

        /// <summary>
        /// Set the pitch diameter private field without notifying property changes.
        /// </summary>
        /// <param name="pitchDiameter"></param>
        public void SetPitchDiameter(float pitchDiameter)
        {
            _pitchDiameter = pitchDiameter;
        }


        private void Rebuild()
        {
            SpurGearBuilder.Build(SceneElement, ShowAxiliaryGeometry);
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(string.Empty);
        }
    }
}