/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;


    public class LookupDataLayer<T> : ABaseDatalayer<T> where T : AIdDataObject, ILookup, new()
    {
        /// <summary>      
        /// Constructor.
        /// </summary>
        /// <param name="database">An initialized Database instance.</param>
        public LookupDataLayer(Database database)
            : base(database)
        {
            _lookupCacheLock = new object();
            _lookupCache = new Dictionary<string, int>();
        }


        /// <summary>
        /// Returns an ID of a lookup item by its name.
        /// </summary>
        /// <returns>An Id of a lookup item or 0.</returns>
        public virtual int GetIdByName(string name, bool bypassCache = false)
        {
            lock (_lookupCacheLock)
            {
                if (String.IsNullOrEmpty(name)) throw new ArgumentException("A name expected.", "name");
                if (String.IsNullOrEmpty(NamePropertyDbColumnName)) throw new Exception("A Name column expected.");

                OperationAllowed(DatabaseOperation.Select);

                if (bypassCache == false && _lookupCache.ContainsKey(name))
                {
                    return _lookupCache[name];
                }

                var id = Database.ExecuteScalarFunction<int>(
                    FunctionBaseName + "_GetIdByName",
                    new[]
                    {
                        new SqlParameter(GetParameterName(NamePropertyDbColumnName), name),
                    },
                    null);

                if (bypassCache == false)
                {
                    _lookupCache.Add(name, id);
                }

                return id;
            }
        }


        private readonly object _lookupCacheLock;
        private readonly Dictionary<string, int> _lookupCache;
    }
}
