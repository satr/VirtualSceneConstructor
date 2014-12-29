using System;
using System.Runtime.Serialization;
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
    [Serializable]
    [KnownType(typeof(CalculationStrategyBase))]
    [KnownType(typeof(NumberOfTeethAndOutsideDiameter))]
    [KnownType(typeof(NumberOfTeethAndPitchDiameter))]
    public class SpurGearEntity : SceneEntity<SpurGear>
    {
        private float _faceWidth;
        private CalculationStrategyBase _calculationStrategy;
        private float _outsideDiameter;
        private int _numberOfTeeth;
        private float _pitchDiameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearEntity" />
        /// </summary>
        public SpurGearEntity()
            : base(Resources.Title_Spur_gear)
        {
            CalculationStrategy = new NumberOfTeethAndOutsideDiameter();
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
                if (Common.Helpers.Math.AssignValue(ref _faceWidth, value, SceneElement, v => SceneElement.FaceWidth = v, 0))
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
                if (SceneElement == null || !Common.Helpers.Math.AssignValue(ref _numberOfTeeth, value, CalculationStrategy.ValidateIsAllowedToChangeNumberOfTeeth, 0)) 
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
                if (SceneElement == null || !Common.Helpers.Math.AssignValue(ref _outsideDiameter, value, CalculationStrategy.ValidateIsAllowedToChangeOutsideDiameter, 0))
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
                if (SceneElement == null || !Common.Helpers.Math.AssignValue(ref _pitchDiameter, value, CalculationStrategy.ValidateIsAllowedToChangePitchDiameter, 0))
                    return;
                SceneElement.PitchDiameter = _pitchDiameter;
                RecalculateGear();
            }
        }

        /// <summary>
        /// The strategy to calculate parameters of the spur gear.
        /// <exception cref="CalculationStrategyNullException">When null is assigned.</exception>
        /// </summary>
        public CalculationStrategyBase CalculationStrategy
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
            RecalculateGear();
        }

        /// <summary>
        /// Build the spur gear shaped <see cref="Polygon" />.
        /// </summary>
        /// <returns>Returns the spur gear shaped <see cref="Polygon" /> as <see cref="SceneElement" />.</returns>
        protected override SpurGear CreateGeometry()
        {
            return SpurGear.Create(4f, 0.5f, 10);
        }


        private void Rebuild()
        {
            SpurGearBuilder.Build(SceneElement);
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(string.Empty);
        }

        #region Calculation strategies types
        
        [XmlInclude(typeof(NumberOfTeethAndPitchDiameter))]
        [XmlInclude(typeof(NumberOfTeethAndOutsideDiameter))]
        public abstract class CalculationStrategyBase
        {
            protected CalculationStrategyBase(string name)
            {
                Name = name;
            }

            [XmlIgnore]
            public string Name { get; private set; }
            public abstract void Calculate(SpurGearEntity spurGearEntity);
            public virtual bool ValidateIsAllowedToChangeNumberOfTeeth() { return false; }
            public virtual bool ValidateIsAllowedToChangeOutsideDiameter() { return false; }
            public virtual bool ValidateIsAllowedToChangePitchDiameter() { return false; }
        }

        public class NumberOfTeethAndPitchDiameter : CalculationStrategyBase
        {
            public NumberOfTeethAndPitchDiameter()
                : base(Resources.Title_By_number_of_teeth_and_pitch_diameter)
            {
            }

            public override void Calculate(SpurGearEntity spurGearEntity)
            {
                throw new NotImplementedException();
            }

            public override bool ValidateIsAllowedToChangeNumberOfTeeth() { return true; }
            public override bool ValidateIsAllowedToChangePitchDiameter() { return true; }
        }

        public class NumberOfTeethAndOutsideDiameter : CalculationStrategyBase
        {
            public NumberOfTeethAndOutsideDiameter()
                : base(Resources.Title_By_number_of_teeth_and_outside_diameter)
            {
            }

            public override void Calculate(SpurGearEntity spurGearEntity)
            {
                var spurGear = spurGearEntity.SceneElement;
                spurGear.DiametralPitch = (spurGear.NumberOfTeeth + 2) / spurGearEntity.OutsideDiameter;
                spurGear.PitchDiameter = spurGearEntity._pitchDiameter = spurGear.NumberOfTeeth / spurGear.DiametralPitch;
                spurGear.ToothThickness = 1.5708f / spurGear.DiametralPitch;
                spurGear.Addendum = 1 / spurGear.DiametralPitch;
                spurGear.WorkingDepth = spurGear.Addendum * 2;
                spurGear.WholeDepth = (float)(spurGear.NumberOfTeeth >= 20
                                                ? 2.2 / spurGear.DiametralPitch + 0.002f
                                                : 2.157 / spurGear.DiametralPitch);
                spurGear.Dedendum = spurGear.WholeDepth - spurGear.Addendum;
                spurGear.CircularPitch = (float)(Math.PI / spurGear.DiametralPitch);
            }

            public override bool ValidateIsAllowedToChangeNumberOfTeeth()
            {
                return true;
            }

            public override bool ValidateIsAllowedToChangeOutsideDiameter()
            {
                return true;
            }
        }

        public class CalculationStrategyNullException : Exception
        {
            public CalculationStrategyNullException()
                : base(Resources.Message_Calculation_strategy_cannot_be_null)
            {
            }
        }

        #endregion
    }
}