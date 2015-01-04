using System;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities.CalculationStrategies
{
    /// <summary>
    ///  The strategy of spur gear properties calculation based on number of teeth and pitch diameter.
    /// </summary>
    public class SpurGearCalculationStrategyByNumberOfTeethAndPitchDiameter : SpurGearCalculationStrategyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearCalculationStrategyByNumberOfTeethAndPitchDiameter" />
        /// </summary>
        public SpurGearCalculationStrategyByNumberOfTeethAndPitchDiameter()
            : base(Resources.Title_By_number_of_teeth_and_pitch_diameter)
        {
        }

        /// <summary>
        /// Calculate parameters of the gear.
        /// </summary>
        /// <param name="spurGearEntity"></param>
        public override void Calculate(SpurGearEntity spurGearEntity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Specifies if the parameter <see cref="SpurGearEntity.NumberOfTeeth" /> can be manually changed.
        /// </summary>
        /// <value>Returns false if changes are allowed.</value>
        public override bool NumberOfTeethReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Specifies if the parameter <see cref="SpurGearEntity.PitchDiameter" /> can be manually changed.
        /// </summary>
        /// <value>Returns false if changes are allowed.</value>
        public override bool PitchDiameterReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Create the <see cref="SpurGear" />.
        /// </summary>
        /// <returns></returns>
        public override SpurGear CreateSpurGear()
        {
            return SpurGear.Create(4f, 0f, 0.5f, 10, 0, 20f);
        }
    }
}