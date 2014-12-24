using System;

namespace VirtualScene.DataComponents.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur when data adapter is not properly configured. 
    /// </summary>
    public class DataAdapterConfigurationException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DataAdapterConfigurationException"/>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public DataAdapterConfigurationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
            
        }
    }
}