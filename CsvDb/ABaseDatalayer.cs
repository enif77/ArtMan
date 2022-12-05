/* (C) 2016 Přemysl Fára */

namespace CsvDb
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;


    /// <summary>
    /// The base of all datalayers.
    /// </summary>
    /// <typeparam name="T">An ABusinessObject type.</typeparam>
    public abstract class ABaseDatalayer<T> where T : AIdDataObject, new()
    {
        #region fields

        /// <summary>
        /// A Database instance used for all database operations.
        /// </summary>
        private readonly Database _database;

        /// <summary>
        /// An instance of a T, used for reflection operations.
        /// </summary>
        private readonly T _typeInstance;

        /// <summary>
        /// Returns a Db column name of a Name column.
        /// Is null for objects without the Name column.
        /// </summary>
        private readonly string _namePropertyDbColumnName;

        private readonly string _dataDirectory;

        // The last known ID.
        private static int _lastId = 0;
        private static object _lastIdLock = new object();

        #endregion


        #region ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">An initialised Database instance to be used for all database operations.</param>
        protected ABaseDatalayer(Database database)
        {
            if (database == null) throw new ArgumentNullException("database");

            _database = database;
            _typeInstance = new T();
            _dataDirectory = Path.Combine(Database.RootDirectoryPath, _typeInstance.DatabaseTableName);

            if (Directory.Exists(_dataDirectory) == false)
            {
                Directory.CreateDirectory(_dataDirectory);
            }

            try
            {
                _namePropertyDbColumnName = ADataObject.GetDbColumnAttribute(_typeInstance.GetColumnsWithTag("Name").FirstOrDefault()).Name;
            }
            catch (Exception)
            {
                ;  // Exceptions ignored. The Name attribuce can be unset or set on an property without the DbColumn attribute.
            }

            DataObjects = new Dictionary<int, T>();
        }

        #endregion


        #region properties

        /// <summary>
        /// A Database instance used for all database operations.
        /// </summary>
        public Database Database
        {
            get { return _database; }
        }
        
        /// <summary>
        /// All previously loaded objects.
        /// </summary>
        public Dictionary<int, T> DataObjects { get; private set; }

        /// <summary>
        /// Returns a Db column name of a Name column.
        /// </summary>
        protected string NamePropertyDbColumnName
        {
            get
            {
                return _namePropertyDbColumnName;
            }
        }
         

        /// <summary>
        /// The last know (the highest) ID.
        /// </summary>
        public static int LastId
        {
            get
            {
                lock (_lastIdLock)
                {
                    return _lastId;
                }
            }

            set
            {
                lock (_lastIdLock)
                {
                    if (value > _lastId) _lastId = value;
                }
            }
        }

        #endregion


        #region public methods

        /// <summary>
        /// Returns all instances of a T.
        /// </summary>
        /// <param name="userDataConsumer">An optional user data consumer instance.</param>
        /// <returns>IEnumerable of all object instances.</returns>
        public virtual IEnumerable<T> GetAll(IDataConsumer<T> userDataConsumer = null)
        {
            try
            {
                var res = new List<T>();
                var consumer = userDataConsumer ?? new DataConsumer<T>(res);

                var tableDirectory = new DirectoryInfo(_dataDirectory);
                foreach (var file in tableDirectory.GetFiles("*.txt"))
                {
                    var de = DataEntity.LoadDataEntity(file.FullName);
                    consumer.CreateInstance(de);
                }

                DataObjects.Clear();

                var maxId = 0;
                foreach (var r in res)
                {
                    if (r.Id > maxId)
                    {
                        maxId = r.Id;
                    }

                    DataObjects.Add(r.Id, r);
                }

                LastId = maxId;

                return res;
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }
                 
        /// <summary>
        /// Returns instance to object by id
        /// </summary>
        /// <param name="id">Id value</param>
        /// <param name="userDataConsumer">An optional user data consumer instance.</param>
        /// <returns>Instance of an object or null.</returns>
        public virtual T Get(int id, IDataConsumer<T> userDataConsumer = null)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("A positive number expected.", "id");

                if (DataObjects.ContainsKey(id))
                {
                    return DataObjects[id];
                }

                T instance = LoadInstance(id, userDataConsumer);
                if (instance == null) return null;

                DataObjects.Add(instance.Id, instance);

                LastId = instance.Id;

                return instance;
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }
             
        /// <summary>
        /// Returns instance to object by id
        /// </summary>
        /// <param name="id">Id value</param>
        /// <param name="userDataConsumer">An optional user data consumer instance.</param>
        /// <returns>Instance of an object or null.</returns>
        public virtual T Reload(int id, IDataConsumer<T> userDataConsumer = null)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("A positive number expected.", "id");

                if (DataObjects.ContainsKey(id))
                {
                    DataObjects.Remove(id);
                }

                T instance = LoadInstance(id, userDataConsumer);
                if (instance == null) return null;
                
                DataObjects.Add(instance.Id, instance);

                LastId = instance.Id;

                return instance;
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }


        /// <summary>
        /// Inserts/updates object in database
        /// </summary>
        /// <param name="obj">Instance to save</param>
        /// <param name="transaction">Instance to SqlTransaction object</param>
        /// <returns>Id of saved instance</returns>
        public virtual int Save(T obj)
        {
            try
            {
                if (obj == null) throw new ArgumentNullException("obj");

                lock (_lastIdLock)
                {
                    if (obj.Id == 0)
                    {
                        obj.Id = LastId + 1;
                    }

                    DataEntity.SaveDataEntity(obj.CreateDataEntity(), GetEntityFilePath(obj.Id));

                    return obj.Id;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }
               
        /// <summary>
        /// Inserts/updates all objects in transaction.
        /// </summary>
        /// <param name="objects">A list of objects.</param>
        public virtual void SaveAll(IEnumerable<T> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            foreach (var obj in objects)
            {
                try
                {
                    Save(obj);
                }
                catch (Exception ex)
                {
                    var exception = new DatabaseException("Can not save a data item.", ex);

                    exception.Data.Add(obj.GetType().Name, obj.ToString());

                    throw exception;
                }
            }
        }
          
        /// <summary>
        /// Deletes object from database
        /// </summary>
        /// <param name="obj">Instance to delete</param>
        public virtual void Delete(T obj)
        {
            try
            {
                if (obj == null) throw new ArgumentNullException("obj");

                if (obj.Id == 0)
                {
                    return;
                }

                DeleteInstance(obj.Id);
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }

        /// <summary>
        /// Deletes object from database.
        /// </summary>
        /// <param name="id">An object ID to delete</param>
        public virtual void Delete(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("An object ID expected.", "id");

                DeleteInstance(id);
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw;
            }
        }

        #endregion


        private T LoadInstance(int id, IDataConsumer<T> userDataConsumer)
        {
            var entityFilePath = GetEntityFilePath(id);
            if (File.Exists(entityFilePath) == false)
            {
                return null;
            }

            var res = new List<T>();
            var consumer = userDataConsumer ?? new DataConsumer<T>(res);
            var de = DataEntity.LoadDataEntity(entityFilePath);
            consumer.CreateInstance(de);

            return res.First();
        }


        private void DeleteInstance(int id)
        {
            if (DataObjects.ContainsKey(id))
            {
                DataObjects.Remove(id);
            }

            var entityFileName = GetEntityFilePath(id);
            if (File.Exists(entityFileName))
            {
                File.Delete(entityFileName);
            }
        }


        private string GetEntityFilePath(int id)
        {
            return Path.Combine(_dataDirectory, String.Format("{0}_{1}.txt", id, _typeInstance.DatabaseTableName));
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void LogError(Exception ex)
        {
            // TODO: Change to the logger.

            Console.Error.WriteLine(ex);

            //try
            //{
            //    Registry.Get<ILoggingDataLayer>().LogError(GetSecurityFunctionCode(DatabaseOperation.Select), ex);
            //}
            //catch (Exception)
            //{
            //    ;  // TODO: Logging can fail?
            //}
        }
    }
}
