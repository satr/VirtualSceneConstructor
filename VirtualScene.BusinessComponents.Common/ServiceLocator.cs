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

        /// <summary>
        /// Clears the internal cash of instances. Used in unit-tests - in TearDown (NUnit) or TestCleanup (MSTest) methods
        /// </summary>
        public static void Clear()
        {
            lock (SyncRoot)
            {
                Instances.Clear();
            }
        }

        /// <summary>
        /// Sets an instance into the internal cash. Used in unit-tests to mock instances returning by the ServiceLocator
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        public static void Set<T>(T obj)
            where T : class, new()
        {
            lock (SyncRoot)
            {
                var type = typeof(T);
                if (Instances.ContainsKey(type))
                    Instances.Remove(type);
                if(obj != null)
                    Instances.Add(type, obj);
            }
        }
    }
}
