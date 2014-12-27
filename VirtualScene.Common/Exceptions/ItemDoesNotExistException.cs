using System;

namespace VirtualScene.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur when removing entity does not exist.
    /// </summary>
    public class ItemDoesNotExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDoesNotExistException" />
        /// </summary>
        /// <param name="message">The error message.</param>
        public ItemDoesNotExistException(string message): base(message)
        {
        }
    }
}