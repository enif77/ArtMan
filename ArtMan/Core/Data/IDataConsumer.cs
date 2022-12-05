/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System.Collections.Generic;
    using System.Data.SqlClient;


    public interface IDataConsumer<T> where T : ADataObject, new()
    {
        /// <summary>
        /// A list of T instances.
        /// </summary>
        ICollection<T> Instances { get; }

        /// <summary>
        /// Creates an object instance from the actual SQL reader state and strores it in the Instances collection.
        /// Data consumer should never call reader.Read() method.
        /// </summary>
        /// <param name="reader">A SQL reader instance with a database row ready to be processed.</param>
        /// <returns>True on succes.</returns>
        bool CreateInstance(SqlDataReader reader);

        /// <summary>
        /// Recreates an object instance from the actual SQL reader state.
        /// Data consumer should never call reader.Read() method.
        /// </summary>
        /// <param name="reader">A SQL reader instance with a database row ready to be processed.</param>
        /// <returns>True on succes.</returns>
        bool RecreateInstance(SqlDataReader reader);
    }
}
