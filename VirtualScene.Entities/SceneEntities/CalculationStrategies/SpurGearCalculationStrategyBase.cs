using System;
using System.Xml.Serialization;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities.CalculationStrategies
{
    /// <summary>
    /// The strategy of spur gear properties calculation
    /// </summary>
    [XmlInclude(typeof(SpurGearCalculationStrategyByNumberOfTeethAndPitchDiameter))]
    [XmlInclude(typeof(SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter))]
    public abstract class SpurGearCalculationStrategyBase
    {
        protected const float HalfPi = (float)(Math.PI / 2);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearCalculationStrategyBase" />
        /// </summary>
        /// <param name="name"></param>
        protected SpurGearCalculationStrategyBase(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the strategy.
        /// </summary>
        [XmlIgnore]
        public string Name { get; private set; }

        /// <summary>
        /// Calculate parameters of the gear.
        /// </summary>
        /// <param name="spurGearEntity"></param>
        public abstract void Calculate(SpurGearEntity spurGearEntity);

        /// <summary>
        /// Specifies if the parameter <see cref="SpurGearEntity.NumberOfTeeth" /> can be manually changed.
        /// </summary>
        /// <value>Returns false if changes are allowed.</value>
        public virtual bool NumberOfTeethReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Specifies if the parameter <see cref="SpurGearEntity.OutsideDiameter" /> can be manually changed.
        /// </summary>
        /// <value>Returns false if changes are allowed.</value>
        public virtual bool OutsideDiameterReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Specifies if the parameter <see cref="SpurGearEntity.PitchDiameter" /> can be manually changed.
        /// </summary>
        /// <value>Returns false if changes are allowed.</value>
        public virtual bool PitchDiameterReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Create the <see cref="SpurGear" />.
        /// </summary>
        /// <returns></returns>
        public abstract SpurGear CreateSpurGear();
    }
}