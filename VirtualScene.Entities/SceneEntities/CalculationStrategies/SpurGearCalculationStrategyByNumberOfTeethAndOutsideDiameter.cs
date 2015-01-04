using System;
using VirtualScene.Entities.Properties;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities.CalculationStrategies
{
    /// <summary>
    /// The strategy of spur gear properties calculation based on number of teeth and outer diameter.
    /// </summary>
    public class SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter : SpurGearCalculationStrategyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter" />
        /// </summary>
        public SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter()
            : base(Resources.Title_By_number_of_teeth_and_outside_diameter)
        {
        }

        /// <summary>
        /// Calculate parameters of the gear.
        /// </summary>
        /// <param name="spurGearEntity"></param>
        public override void Calculate(SpurGearEntity spurGearEntity)
        {
            var spurGear = spurGearEntity.SceneElement;
            spurGear.DiametralPitch = (spurGear.NumberOfTeeth + 2) / spurGearEntity.OutsideDiameter;
            spurGear.PitchDiameter = spurGear.NumberOfTeeth / spurGear.DiametralPitch;
            spurGearEntity.SetPitchDiameter(spurGear.PitchDiameter);
            spurGear.ToothThickness = HalfPi / spurGear.DiametralPitch;
            spurGear.Addendum = 1 / spurGear.DiametralPitch;
            spurGear.WorkingDepth = spurGear.Addendum * 2;
            spurGear.WholeDepth = (float)(spurGear.NumberOfTeeth >= 20
                ? 2.2 / spurGear.DiametralPitch + 0.002f
                : 2.157 / spurGear.DiametralPitch);
            spurGear.Dedendum = spurGear.WholeDepth - spurGear.Addendum;
            spurGear.CircularPitch = (float)((Math.PI * spurGear.PitchDiameter) / spurGear.NumberOfTeeth);                
        }

        /// <summary>
        /// Validate if the parameter <see cref="SpurGearEntity.NumberOfTeeth" /> can be manually changed.
        /// </summary>
        /// <returns>Returns true if changes are allowed.</returns>
        public override bool ValidateIsAllowedToChangeNumberOfTeeth()
        {
            return true;
        }

        /// <summary>
        /// Validate if the parameter <see cref="SpurGearEntity.OutsideDiameter" /> can be manually changed.
        /// </summary>
        /// <returns>Returns true if changes are allowed.</returns>
        public override bool ValidateIsAllowedToChangeOutsideDiameter()
        {
            return true;
        }

        /// <summary>
        /// Create the <see cref="SpurGear" />.
        /// </summary>
        /// <returns></returns>
        public override SpurGear CreateSpurGear()
        {
            return SpurGear.Create(0f, 4f, 0.5f, 10, 0);
        }
    }
}