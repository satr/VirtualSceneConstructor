using System;

namespace VirtualScene.DataComponents.Common.Exceptions
{
    /// <summary>
    /// Base class for file-system related exceptions
    /// </summary>
    public abstract class FileSystemException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemException" />
        /// </summary>
        /// <param name="message"></param>
        protected FileSystemException(string message) : base(message)
        {
        }
    }
}