using System;
using System.Collections.Generic;

namespace VirtualScene.BusinessComponents.Common
{
    /// <summary>
    /// Creates and returns instances of types
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly IDictionary<Type, object> Instances = new Dictionary<Type, object>();
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Create or return already created instance of the specified type
        /// </summary>
        /// <typeparam name="T">The type which instance should be returned</typeparam>
        /// <returns>The instance of the required type</returns>
        public static T Get<T>()
            where T: class, new()
        {
            var type = typeof (T);
            lock (SyncRoot)
            {
                if(!Instances.ContainsKey(type))
                    Instances.Add(type, new T());
                return (T) Instances[type];
            }
        }
    }
}
