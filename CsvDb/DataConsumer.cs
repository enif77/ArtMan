/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    
    /// <summary>
    /// Consumes data from a data entity and construts T instances from them.
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
        /// Creates an object instance from a data entity and strores it in the Instances collection.
        /// </summary>
        /// <param name="entity">An entity instance to be processed.</param>
        /// <returns>True on succes.</returns>
        public virtual bool CreateInstance(DataEntity entity)
        {
            var instance = new T();

            GetData(entity, instance);

            Instances.Add(instance);

            return true;
        }

        /// <summary>
        /// Recreates an object instance from an entity.
        /// </summary>
        /// <param name="entity">A data entity to be processed.</param>
        /// <returns>True on succes.</returns>
        public virtual bool RecreateInstance(DataEntity entity)
        {
            if (Instances.Count == 0) throw new Exception("An object instance for recreation expected.");

            // Expecting an instance in the list of instances.
            GetData(entity, Instances.FirstOrDefault());

            return true;
        }

        /// <summary>
        /// Extracts data from a data entity to the given instance.
        /// </summary>
        /// <param name="entity">A data entity ready to give data.</param>
        /// <param name="instance">An instance of a target object.</param>
        private static void GetData(DataEntity entity, T instance)
        {
            // For all DB columns...
            foreach (var column in instance.DatabaseColumns)
            {
                // Get the instance of this column attribute.
                var attribute = ADataObject.GetDbColumnAttribute(column);

                var columnType = column.PropertyType;
                switch (columnType.Name)
                {
                    // TODO: Nullable types?

                    case "Int32": column.SetValue(instance, entity.GetInt32Value(attribute.Name)); break;
                    case "Boolean": column.SetValue(instance, entity.GetBoolValue(attribute.Name)); break;
                    case "Decimal": column.SetValue(instance, entity.GetDecimalValue(attribute.Name)); break;
                    case "DateTime": column.SetValue(instance, entity.GetDateTimeValue(attribute.Name)); break;
                    case "String": column.SetValue(instance, entity.GetValue(attribute.Name)); break;
                }
            }
        }
    }
}
