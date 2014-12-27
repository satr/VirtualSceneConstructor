using System;

namespace VirtualScene.Common
{
    /// <summary>
    /// The controller performing an operation specific for the type.
    /// </summary>
    public interface IOperationController
    {
        /// <summary>
        /// The type-key defining the operation controller.
        /// </summary>
        Type TypeKey { get; }
    }
}