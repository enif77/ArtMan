/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    
    /// <summary>
    /// Consumes data from a SqlDataReader and construts T instances from them.
    /// </summary>
    /// <typeparam name="T">A constructed instnaces data type.</typeparam>
    public class DataConsumer<T> : IDataConsumer<T> where T : ADataObject, new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="instances">A collection, where created instances are stored.</param>
        public DataConsumer(ICollection<T> instances)
        {
            if (instances == null) throw new ArgumentNullException("instances");

            Instances = instances;
        }
        

        /// <summary>
        /// A list of T instances.
        /// </summary>
        public ICollection<T> Instances
        {
            get;
            private set;
        }


        /// <summary>
        /// Creates an object instance from the actual SQL reader state and stores it in the Instances collection.
        /// Data consumer should never call reader.Read() method.
        /// </summary>
        /// <param name="reader">A SQL reader instance with a database row ready to be processed.</param>
        /// <returns>True on succes.</returns>
        public virtual bool CreateInstance(SqlDataReader reader)
        {
            var instance = new T();

            GetData(reader, instance);

            Instances.Add(instance);

            return true;
        }
        
        /// <summary>
        /// Recreates an object instance from the actual SQL reader state.
        /// Data consumer should never call reader.Read() method.
        /// </summary>
        /// <param name="reader">A SQL reader instance with a database row ready to be processed.</param>
        /// <returns>True on succes.</returns>
        public virtual bool RecreateInstance(SqlDataReader reader)
        {
            if (Instances.Count == 0) throw new Exception("An object instance for recreation expected.");

            // Expecting an instance in the list of instances.
            GetData(reader, Instances.FirstOrDefault());

            return true;
        }

        /// <summary>
        /// Extracts data from DB reader to the given instance.
        /// </summary>
        /// <param name="reader">A DB reader ready to give data.</param>
        /// <param name="instance">An instance of a target object.</param>
        private static void GetData(SqlDataReader reader, T instance)
        {
            // For all DB columns...
            foreach (var column in instance.DatabaseColumns)
            {
                // Get the instance of this column attribute.
                var attribute = ADataObject.GetDbColumnAttribute(column);

                // Skip ignored columns.
                if (attribute.IsIgnored) continue;

                var columnData = reader[attribute.Name]; // Can throw IndexOutOfRangeException.
                var columnType = column.PropertyType;

                // Get a value from the reader object and convert it it to a property type.
                column.SetValue(instance, (columnData is DBNull)
                    ? (columnType.IsValueType ? Activator.CreateInstance(columnType) : null)
                    : columnData);

                //// Get a value from the reader object and convert it it to a property type.
                //column.SetValue(instance, (columnData is DBNull)
                //    ? (columnType.IsValueType ? Activator.CreateInstance(columnType) : null)
                //    : Convert.ChangeType(columnData, columnType));
            }
        }

        ///// <summary>
        ///// Creates an object instance from the actual SQL reader state and stores it in the Instances collection.
        ///// Data consumer should never call reader.Read() method.
        ///// </summary>
        ///// <param name="reader">A SQL reader instance with a database row ready to be processed.</param>
        ///// <returns>True on succes.</returns>
        //public virtual bool CreateSimpleInstance(SqlDataReader reader)
        //{
        //    return CreateInstance(reader);
        //}
    }
}
