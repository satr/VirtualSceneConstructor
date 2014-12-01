using System;
using System.Collections.Generic;

namespace VirtualScene.Common
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

        /// <summary>
        /// Warnings for the operation.
        /// </summary>
        IList<string> Warnings { get; }

        /// <summary>
        /// Add an error occured during operation
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">Arguments of the formatted message (optional)</param>
        void AddError(string format, params object[] args);

        /// <summary>
        /// Add an error occured during operation
        /// </summary>
        /// <param name="exception">The exception occured during the operation.</param>
        void AddError(Exception exception);

        /// <summary>
        /// Add an warning for the operation
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">Arguments of the formatted message (optional)</param>
        void AddWarning(string format, params object[] args);
    }
}