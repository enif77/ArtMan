/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Core.Injektor
{
    using System;
    using System.Collections.Generic;

    
    public static class Registry
    {
        private static readonly object _lock = new object();
        private static readonly Dictionary<Type, object> _instancesDictionary = new Dictionary<Type, object>();


        /// <summary>
        /// Checks, if an instance of a specific type is already registered.
        /// </summary>
        /// <typeparam name="T">A type of an instance.</typeparam>
        /// <returns>True, if an instance of a certain type is already registered.</returns>
        public static bool IsRegistered<T>() where T : class
        {
            lock (_lock)
            {
                return _instancesDictionary.ContainsKey(typeof(T));
            }
        }
        
        /// <summary>
        /// Registers an instance of a type.
        /// </summary>
        /// <typeparam name="T">A type of an instance.</typeparam>
        /// <param name="instance">An instance.</param>
        public static void RegisterInstance<T> (T instance) where T : class
        {
            lock (_lock)
            {
                var instanceType = instance.GetType();

                // Do not allow to register a type more than once.
                if (_instancesDictionary.ContainsKey(instanceType))
                {
                    throw new InstanceAlreadyRegisteredException(instanceType.FullName);
                }

                _instancesDictionary.Add(instanceType, instance);    
            }
        }

        /// <summary>
        /// Gets an instance of a specific type.
        /// </summary>
        /// <typeparam name="T">A type of an instance.</typeparam>
        /// <returns>An instance of a certain type.</returns>
        public static T Get<T>() where T : class
        {
            lock (_lock)
            {
                var instanceType = typeof(T);

                if (_instancesDictionary.ContainsKey(instanceType))
                {
                    return _instancesDictionary[instanceType] as T;
                }

                throw new InstanceNotRegisteredException(instanceType.FullName);    
            }
        }

        /// <summary>
        /// Removes an instance of a specific type.
        /// </summary>
        /// <typeparam name="T">A type of an instance.</typeparam>
        public static void Remove<T>() where T : class
        {
            lock (_lock)
            {
                var instanceType = typeof(T);

                if (_instancesDictionary.ContainsKey(instanceType))
                {
                    _instancesDictionary.Remove(instanceType);
                }

                throw new InstanceNotRegisteredException(instanceType.FullName);    
            }
        }

        /// <summary>
        /// Removes all registered instances.
        /// </summary>
        public static void RemoveAll()
        {
            lock (_lock)
            {
                _instancesDictionary.Clear();    
            }
        }
    }
}
