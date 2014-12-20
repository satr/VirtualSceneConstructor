using System;
using System.Collections.Generic;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters
{
    /// <summary>
    /// The pool providing instances of data-adapters
    /// </summary>
    public class DataAdaptersPool
    {
        private readonly IDictionary<Type, object> _registeredDataAdapters = new Dictionary<Type, object>();

        /// <summary>
        /// Creates a new instance of DataAdaptersPool
        /// </summary>
        public DataAdaptersPool()
        {
            _registeredDataAdapters.Add(typeof(IStage), new StageFileSystemDataAdapter());
        }

        /// <summary>
        /// The data-adapter for accessing to file-system.
        /// </summary>
        /// <returns>The path-manager</returns>
        public virtual IDataAdapter<T> GetFileSystemDataAdapter<T>()
        {
            var dataAdapterType = typeof (T);
            if (_registeredDataAdapters.ContainsKey(dataAdapterType))
                return (IDataAdapter<T>) _registeredDataAdapters[dataAdapterType];
            throw new InvalidOperationException(string.Format(Resources.Message_A_data_adapter_for_the_type_N_is_not_registered, dataAdapterType));
        }
    }
}
