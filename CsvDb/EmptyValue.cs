/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System;
    using System.Collections.Concurrent;


    /// <summary>
    /// Helper class for creating empty values used in SingleSelect controls in WPF
    /// </summary>
    /// <typeparam name="T">Type of BusinessObject</typeparam>
    public static class EmptyValue<T> where T : class, ILookup, new()
    {
        #region fields

        /// <summary>
        /// Storage containers for required and optional values
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> RequiredValues = new ConcurrentDictionary<Type, object>();
        private static readonly ConcurrentDictionary<Type, object> OptionalValues = new ConcurrentDictionary<Type, object>();

        #endregion

        #region properties

        /// <summary>
        /// Represents empty instance of BusinessObject for Required item
        /// </summary>
        public static T Required
        {
            get
            {
                RequiredValues.TryAdd(typeof(T), new T { Name = "<choose>" });
                return (T)RequiredValues[typeof(T)];
            }
        }

        /// <summary>
        /// Represents empty instance of BusinessObject for Optional item
        /// </summary>
        public static T Optional
        {
            get
            {
                OptionalValues.TryAdd(typeof(T), new T { Name = "<none>" });
                return (T)OptionalValues[typeof(T)];
            }
        }

        #endregion
    }
}
