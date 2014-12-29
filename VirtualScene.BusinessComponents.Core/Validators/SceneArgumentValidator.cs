using System;
using VirtualScene.BusinessComponents.Core.Properties;

namespace VirtualScene.BusinessComponents.Core.Validators
{
    /// <summary>
    /// Validator for arguments for creating the virtual scene
    /// </summary>
    public class SceneArgumentValidator
    {
        /// <summary>
        /// Validates argumets for creating of the virtual scene
        /// </summary>
        /// <param name="width">Width of the scene</param>
        /// <param name="height">FaceWidth of the scene</param>
        /// <param name="bitDepth">Color depth of the scene</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValidateArguments(int width, int height, int bitDepth)
        {
            if (width < Constants.Scene.MinimumWidth)
                throw new ArgumentOutOfRangeException("width", string.Format(Resources.Message_ValidateSceneArguments_Minimum_width_value_N, Constants.Scene.MinimumWidth));
            if (height < Constants.Scene.MinimumHeight)
                throw new ArgumentOutOfRangeException("height", string.Format(Resources.Message_ValidateSceneArguments_Minimum_height_value_N, Constants.Scene.MinimumHeight));
            if (bitDepth < Constants.Scene.MinimumColorDepth)
                throw new ArgumentOutOfRangeException("bitDepth", Resources.Message_ValidateSceneArguments_Minimum_bitDepth_value_N);
            if (bitDepth > Constants.Scene.MaximumColorDepth)
                throw new ArgumentOutOfRangeException("bitDepth", Resources.Message_ValidateSceneArguments_Maximum_bitDepth_value_N);
        }
    }
}