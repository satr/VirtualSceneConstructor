using System;
using System.Linq;
using System.Security.Policy;

namespace VirtualScene.Common.Helpers
{
    /// <summary>
    /// Helper methods for math operations.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Compare <see cref="float" /> types.
        /// </summary>
        /// <param name="x">Value 1</param>
        /// <param name="y">Value 2</param>
        /// <returns>Returns resultat of comparison.</returns>
        public static bool FloatEqual(float x, float y)
        {
            return System.Math.Abs(x - y) < float.Epsilon;
        }

        /// <summary>
        /// Assign newValue to currentValue if they are different.
        /// </summary>
        /// <param name="currentValue">The value to be changed.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="illegalValues">Values which should be skipped (not to be assigned).</param>
        /// <returns>Returns true if the new value has been assigned.</returns>
        public static bool AssignValue(ref float currentValue, float newValue, params float[] illegalValues)
        {
            return AssignValue(ref currentValue, newValue, () => true, illegalValues);
        }

        /// <summary>
        /// Assign newValue to currentValue if they are different.
        /// </summary>
        /// <param name="currentValue">The value to be changed.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="validate">When the validator returns false - newValue should be skipped (not to be assigned).</param>
        /// <param name="illegalValues">Values which should be skipped (not to be assigned).</param>
        /// <returns>Returns true if the new value has been assigned.</returns>
        public static bool AssignValue(ref float currentValue, float newValue, Func<bool> validate, params float[] illegalValues)
        {
            if (!validate() || FloatEqual(currentValue, newValue)
                || illegalValues.Any(illegalValue => FloatEqual(newValue, illegalValue)))
                return false;
            
            currentValue = newValue;
            return true;
        }

        /// <summary>
        /// Assign newValue to currentValue if they are different.
        /// </summary>
        /// <param name="currentValue">The value to be changed.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="illegalValues">Values which should be skipped (not to be assigned).</param>
        /// <returns>Returns true if the new value has been assigned.</returns>
        public static bool AssignValue(ref int currentValue, int newValue, params int[] illegalValues)
        {
            return AssignValue(ref currentValue, newValue, () => true, illegalValues);
        }

        /// <summary>
        /// Assign newValue to currentValue if they are different.
        /// </summary>
        /// <param name="currentValue">The value to be changed.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="validate">When the validator returns false - newValue should be skipped (not to be assigned).</param>
        /// <param name="illegalValues">Values which should be skipped (not to be assigned).</param>
        /// <returns>Returns true if the new value has been assigned.</returns>
        public static bool AssignValue(ref int currentValue, int newValue, Func<bool> validate, params int[] illegalValues)
        {
            if (!validate() || currentValue == newValue 
                || illegalValues.Any(illegalValue => newValue == illegalValue))
                return false;
            
            currentValue = newValue;
            return true;
        }

        /// <summary>
        /// Assign one newValue to currentValue if they are different.
        /// </summary>
        /// <param name="currentValue">The value to be changed.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="obj">When the <see cref="obj" /> is not null - the <see cref="setEntityValueAction" /> is executed.
        /// When the <see cref="obj" /> is null - the "0" is assigned to the <see cref="currentValue" />.</param>
        /// <param name="setEntityValueAction">The action executed whe the <see cref="obj" /> is not null.</param>
        /// <param name="minOuterValue">When <see cref="minOuterValue" /> not null - <see cref="newValue" /> should be more than <see cref="minOuterValue" />. Otherwise newValue which should be skipped (not to be assigned).</param>
        /// <returns>Returns true if the new value has been assigned.</returns>
        public static bool AssignValue(ref float currentValue, float newValue, object obj, Action<float> setEntityValueAction, float? minOuterValue = null)
        {
            if (FloatEqual(newValue, currentValue)
                || (minOuterValue.HasValue && !(newValue > minOuterValue)))
                return false;
            currentValue = (obj == null) ? 0 : newValue;
            if (obj != null)
                setEntityValueAction(currentValue);
            return true;
        }

    }
}
