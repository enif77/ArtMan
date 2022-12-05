/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System;
    using System.Collections.Generic;


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

                if (bypassCache == false && _lookupCache.ContainsKey(name))
                {
                    return _lookupCache[name];
                }

                foreach (var dataObject in DataObjects.Values)
                {
                    if (dataObject.Name == name)
                    {
                        if (bypassCache == false)
                        {
                            _lookupCache.Add(name, dataObject.Id);
                        }

                        return dataObject.Id;
                    }
                }
                
                return 0;
            }
        }


        private readonly object _lookupCacheLock;
        private readonly Dictionary<string, int> _lookupCache;
    }
}
