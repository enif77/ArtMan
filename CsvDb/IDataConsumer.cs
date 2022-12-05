/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System.Collections.Generic;


    public interface IDataConsumer<T> where T : ADataObject, new()
    {
        /// <summary>
        /// A list of T instances.
        /// </summary>
        ICollection<T> Instances { get; }

        /// <summary>
        /// Creates an object instance from a data entity and strores it in the Instances collection.
        /// </summary>
        /// <param name="entity">An entity instance to be processed.</param>
        /// <returns>True on succes.</returns>
        bool CreateInstance(DataEntity entity);

        /// <summary>
        /// Recreates an object instance from an entity.
        /// </summary>
        /// <param name="entity">A data entity to be processed.</param>
        /// <returns>True on succes.</returns>
        bool RecreateInstance(DataEntity entity);
    }
}
