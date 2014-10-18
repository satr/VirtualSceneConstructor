using System.Collections.Generic;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The result of the operation with the value the operation returns.
    /// </summary>
    public interface IActionResult
    {
        /// <summary>
        /// The result of the action.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Errors occured during the operations.
        /// </summary>
        IList<string> Errors { get; }
    }
}