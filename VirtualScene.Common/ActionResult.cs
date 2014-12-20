using System;
using System.Collections.Generic;
using VirtualScene.Common.Properties;

namespace VirtualScene.Common
{
    /// <summary>
    /// The result of the operation with the value the operation returns.
    /// </summary>
    public class ActionResult<T> : IActionResult
    {
        private readonly string _operationTitle;

        /// <summary>
        /// Creates a new instance of the action result
        /// </summary>
        public ActionResult(): this(Resources.Message_Not_defined)
        {
        }

        /// <summary>
        /// Creates a new instance of the action result
        /// </summary>
        /// <param name="operationTitle">The name of the operation.</param>
        public ActionResult(string operationTitle)
        {
            _operationTitle = operationTitle;
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        /// <summary>
        /// Errors occured during the operation.
        /// </summary>
        public IList<string> Errors { get; private set; }
        
        /// <summary>
        /// Warnings for the operation.
        /// </summary>
        public IList<string> Warnings { get; private set; }

        /// <summary>
        /// Add an error occured during operation
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">Arguments of the formatted message (optional)</param>
        public void AddError(string format, params object[] args)
        {
            Errors.Add(string.Format(format, args));
        }

        /// <summary>
        /// Add an error occured during operation
        /// </summary>
        /// <param name="exception">The exception occured during the operation.</param>
        public void AddError(Exception exception)
        {
            AddError(Resources.Message_Error_occured_during_the_operation_M_N, _operationTitle, exception.Message);
        }

        /// <summary>
        /// The value of the action
        /// </summary>
        public virtual T Value { get; set; }

        /// <summary>
        /// The result of the action.
        /// </summary>
        public bool Success { get { return Errors.Count == 0; } }

        /// <summary>
        /// Add an warning for the operation
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">Arguments of the formatted message (optional)</param>
        public void AddWarning(string format, params object[] args)
        {
            Warnings.Add(string.Format(format, args));
        }

        /// <summary>
        /// Combine the action-result with another action result.
        /// </summary>
        /// <param name="actionResult">The action-result which errors and warnings are added to the current action-result.</param>
        public void CombineWith(IActionResult actionResult)
        {
            foreach (var error in actionResult.Errors)
                AddError(error);
            foreach (var warning in actionResult.Warnings)
                AddWarning(warning);
        }
    }
}
