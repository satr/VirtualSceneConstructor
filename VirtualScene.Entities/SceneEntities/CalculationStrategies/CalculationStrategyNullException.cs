using System;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities.CalculationStrategies
{
    /// <summary>
    /// Represents errors when there is no calculation strategy is defined.
    /// </summary>
    public class CalculationStrategyNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationStrategyNullException" />
        /// </summary>
        public CalculationStrategyNullException()
            : base(Resources.Message_Calculation_strategy_cannot_be_null)
        {
        }
    }
}