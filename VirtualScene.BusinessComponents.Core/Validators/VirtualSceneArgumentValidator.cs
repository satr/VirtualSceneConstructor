using System;
using VirtualScene.BusinessComponents.Core.Properties;

namespace VirtualScene.BusinessComponents.Core.Validators
{
    /// <summary>
    /// Validator for arguments for creating the virtual scene
    /// </summary>
    public class VirtualSceneArgumentValidator
    {
        /// <summary>
        /// Validates argumets for creating of the virtual scene
        /// </summary>
        /// <param name="width">Width of the scene</param>
        /// <param name="height">Height of the scene</param>
        /// <param name="bitDepth">Color depth of the scene</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValidateSceneArguments(int width, int height, int bitDepth)
        {
            if (width < Constants.VirtualScene.MinimumWidth)
                throw new ArgumentOutOfRangeException("width", string.Format(Resources.Message_ValidateSceneArguments_Minimum_width_value_N, Constants.VirtualScene.MinimumWidth));
            if (height < Constants.VirtualScene.MinimumHeight)
                throw new ArgumentOutOfRangeException("height", string.Format(Resources.Message_ValidateSceneArguments_Minimum_height_value_N, Constants.VirtualScene.MinimumHeight));
            if (bitDepth < Constants.VirtualScene.MinimumColorDepth)
                throw new ArgumentOutOfRangeException("bitDepth", Resources.Message_ValidateSceneArguments_Minimum_bitDepth_value_N);
            if (bitDepth > Constants.VirtualScene.MaximumColorDepth)
                throw new ArgumentOutOfRangeException("bitDepth", Resources.Message_ValidateSceneArguments_Maximum_bitDepth_value_N);
        }
    }
}