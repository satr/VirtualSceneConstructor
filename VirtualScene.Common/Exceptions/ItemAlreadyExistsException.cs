using System;

namespace VirtualScene.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur when adding entity already exists.
    /// </summary>
    public class ItemAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemAlreadyExistsException" />
        /// </summary>
        /// <param name="message">The error message.</param>
        public ItemAlreadyExistsException(string message): base(message)
        {
        }
    }
}