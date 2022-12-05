/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    using ArtMan.Core.Injektor;
    //using Ezpada.Core.Security;


    /// <summary>
    /// The base of all datalayers.
    /// </summary>
    /// <typeparam name="T">An ABusinessObject type.</typeparam>
    public abstract class ABaseDatalayer<T> where T : ADataObject, new()
    {
        #region constants

        /// <summary>
        /// Database operations.
        /// </summary>
        protected enum DatabaseOperation
        {
            /// <summary>
            /// For Get, GetAll, etc.
            /// </summary>
            Select,

            /// <summary>
            /// For Save and SaveAll, when a new object is saved into a database.
            /// </summary>
            Insert,

            /// <summary>
            /// For Save and SaveAll, when an existing object is saved into a database.
            /// </summary>
            Update,

            /// <summary>
            /// For Delete. 
            /// </summary>
            Delete
        }

        #endregion


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
        /// A base of a stored procedure name.
        /// </summary>
        private readonly string _storedProcedureBaseName;

        /// <summary>
        /// A base of a function name.
        /// </summary>
        private readonly string _functionBaseName;

        /// <summary>
        /// Returns a Db column name of a Name column.
        /// Is null for objects without the Name column.
        /// </summary>
        private readonly string _namePropertyDbColumnName;

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

            var baseName = _typeInstance.DatabaseTableName;

            _storedProcedureBaseName = "sp" + baseName;
            _functionBaseName = "fn" + baseName;

            try
            {
                _namePropertyDbColumnName = ADataObject.GetDbColumnAttribute(_typeInstance.GetColumnsWithTag("Name").FirstOrDefault()).Name;
            }
            catch (Exception)
            {
                ;  // Exceptions ignored. The Name attribuce can be unset or set on an property without the DbColumn attribute.
            }
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
        /// If true, the AuthorizationManager security is nod used.
        /// False by default (the security IS used).
        /// </summary>
        protected virtual bool BypassSecurity
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a base of a stored procedure name.
        /// </summary>
        protected virtual string StoredProcedureBaseName
        {
            get
            {
                return _storedProcedureBaseName;  
            } 
        }

        /// <summary>
        /// Gets a base of a function name.
        /// </summary>
        protected virtual string FunctionBaseName
        {
            get
            {
                return _functionBaseName;
            }
        }
        
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

        #endregion


        #region public methods

        /// <summary>
        /// Returns a security function code for a database operation.
        /// </summary>
        /// <param name="operation">A database opertation.</param>
        /// <returns>A security function code.</returns>
        protected virtual string GetSecurityFunctionCode(DatabaseOperation operation)
        {
            return operation.ToString();
        }

        /// <summary>
        /// Checks, if an operation is allowed for a certain user.
        /// </summary>
        /// <param name="operation">A DatabaseOperation code.</param>
        protected void OperationAllowed(DatabaseOperation operation)
        {
            if (BypassSecurity) return;

            OperationAllowedImplementation(operation);
        }

        /// <summary>
        /// Checks, if an operation is allowed for a certain user - implementation.
        /// </summary>
        /// <param name="operation">A DatabaseOperation code.</param>
        protected virtual void OperationAllowedImplementation(DatabaseOperation operation)
        {
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="functionCode"></param>
        /// <param name="ex"></param>
        protected virtual void LogError(string functionCode, Exception ex)
        {
            // No logging by default.
        }

        /// <summary>
        /// Returns all instances of a T.
        /// </summary>
        /// <param name="userDataConsumer">An optional user data consumer instance.</param>
        /// <returns>IEnumerable of all object instances.</returns>
        public virtual IEnumerable<T> GetAll(IDataConsumer<T> userDataConsumer = null)
        {
            try
            {
                OperationAllowed(DatabaseOperation.Select);

                var res = new List<T>();

                var consumer = userDataConsumer ?? new DataConsumer<T>(res);

                _database.ExecuteReader(
                    GetSelectStoredProcedureName(),
                    CreateSelectListParameters(),
                    consumer.CreateInstance,
                    null);

                return res;
            }
            catch (Exception ex)
            {
                LogError(GetSecurityFunctionCode(DatabaseOperation.Select), ex);

                throw;
            }
        }

        /// <summary>
        /// Returns instance to object by id
        /// </summary>
        /// <param name="id">Id value</param>
        /// <param name="transaction">An optional SQL transaction.</param>
        /// <returns>Instance to object</returns>
        public virtual T Get(int id, SqlTransaction transaction = null)
        {
            return Get(id, null, transaction);
        }

        /// <summary>
        /// Returns instance to object by id
        /// </summary>
        /// <param name="id">Id value</param>
        /// <param name="userDataConsumer">An optional user data consumer instance.</param>
        /// <param name="transaction">An optional SQL transaction.</param>
        /// <returns>Instance to object</returns>
        public virtual T Get(int id, IDataConsumer<T> userDataConsumer, SqlTransaction transaction = null)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("A positive number expected.", "id");

                OperationAllowed(DatabaseOperation.Select);

                var res = new List<T>();

                var consumer = userDataConsumer ?? new DataConsumer<T>(res);
               
                 _database.ExecuteReader(
                    GetSelectDetailsStoredProcedureName(),
                    CreateIdParameters(id),
                    consumer.CreateInstance, 
                    transaction);
                
                return res.FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogError(GetSecurityFunctionCode(DatabaseOperation.Select), ex);

                throw;
            }
        }

        /// <summary>
        /// Inserts/updates object in database
        /// </summary>
        /// <param name="obj">Instance to save</param>
        /// <param name="transaction">Instance to SqlTransaction object</param>
        /// <returns>Id of saved instance</returns>
        public virtual int Save(T obj, SqlTransaction transaction = null)
        {
            var operation = DatabaseOperation.Insert;
            try
            {
                if (obj == null) throw new ArgumentNullException("obj");

                var iid = obj as IId;
                if (iid == null)
                {
                    OperationAllowed(operation);

                    return _database.ExecuteScalar(GetInsertStoredProcedureName(), CreateInsertParameters(obj), transaction);
                }

                if (iid.Id == 0)
                {
                    OperationAllowed(operation);

                    return iid.Id = _database.ExecuteScalar(GetInsertStoredProcedureName(), CreateInsertParameters(obj), transaction);
                }

                operation = DatabaseOperation.Update;

                OperationAllowed(operation);

                return _database.ExecuteScalar(GetUpdateStoredProcedureName(), CreateUpdateParameters(obj), transaction);
            }
            catch (Exception ex)
            {
                LogError(GetSecurityFunctionCode(operation), ex);
                
                throw;
            }
        }
        
        /// <summary>
        /// Inserts/updates all objects in transaction.
        /// </summary>
        /// <param name="objects">A list of objects.</param>
        /// <param name="transaction">A transaction.</param>
        public virtual void SaveAll(IEnumerable<T> objects, SqlTransaction transaction = null)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            if (objects.Any() == false) return;

            if (transaction == null)
            {
                _database.DoInTransaction(SaveAllOperation, objects);
            }
            else
            {
                SaveAllOperation(transaction, objects);
            }
        }
        
        /// <summary>
        /// Deletes object from database
        /// </summary>
        /// <param name="obj">Instance to delete</param>
        /// <param name="transaction">Instance to SqlTransaction object</param>
        public virtual void Delete(T obj, SqlTransaction transaction = null)
        {
            try
            {
                if (obj == null) throw new ArgumentNullException("obj");

                OperationAllowed(DatabaseOperation.Delete);

                _database.ExecuteNonQuery(GetDeleteStoredProcedureName(), CreateIdParameters(obj), transaction);
            }
            catch (Exception ex)
            {
                LogError(GetSecurityFunctionCode(DatabaseOperation.Delete), ex);

                throw;
            }
        }

        /// <summary>
        /// Deletes object from database.
        /// </summary>
        /// <param name="id">An object ID to delete</param>
        /// <param name="transaction">Instance to SqlTransaction object</param>
        public virtual void Delete(int id, SqlTransaction transaction = null)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("An object ID expected.", "id");

                OperationAllowed(DatabaseOperation.Delete);

                var iid = _typeInstance as IId;
                if (iid == null) throw new NotSupportedException("Object does not contain ID");

                _database.ExecuteNonQuery(GetDeleteStoredProcedureName(), CreateIdParameters(iid.Id), transaction);
            }
            catch (Exception ex)
            {
                LogError(GetSecurityFunctionCode(DatabaseOperation.Delete), ex);

                throw;
            }
        }

        /// <summary>
        /// Reloads an object from the database.
        /// </summary>
        /// <param name="obj">An object instance to be reloaded from a database.</param>
        /// <param name="transaction">An optional SQL transaction.</param>
        public virtual void Reload(T obj, SqlTransaction transaction = null)
        {
            Reload(obj, null, transaction);
        }

        /// <summary>
        /// Reloads an object from the database.
        /// </summary>
        /// <param name="obj">An object instance to be reloaded from a database.</param>
        /// <param name="userDataConsumer">An optional user data consumer instance.</param>
        /// <param name="transaction">An optional SQL transaction.</param>
        public virtual void Reload(T obj, IDataConsumer<T> userDataConsumer, SqlTransaction transaction = null)
        {
            try
            {
                var iid = obj as IId;
                if (iid == null) throw new NotSupportedException("Object does not contain ID");

                OperationAllowed(DatabaseOperation.Select);

                var consumer = userDataConsumer ?? new DataConsumer<T>(new List<T> { obj });

                _database.ExecuteReader(
                   GetSelectDetailsStoredProcedureName(),
                   CreateIdParameters(iid.Id),
                   consumer.RecreateInstance,
                   transaction);
            }
            catch (Exception ex)
            {
                LogError(GetSecurityFunctionCode(DatabaseOperation.Select), ex);

                throw;
            }
        }

        #endregion


        #region non-public methods

        /// <summary>
        /// Creates parameters for a SELECT database operation. 
        /// </summary>
        /// <returns>A list of SqlParameters.</returns>
        protected virtual SqlParameter[] CreateSelectListParameters()
        {
            return null;
        }

        /// <summary>
        /// Creates parameters for a INSERT database operation. 
        /// </summary>
        /// <returns>A list of SqlParameters.</returns>
        protected SqlParameter[] CreateInsertParameters(ADataObject instance)
        {
            return CreateInsertOrUpdateParameters(instance, instance is IId);  // TODO: Insert parameters for non IId objects?
        }

        /// <summary>
        /// Creates parameters for a UPDATE database operation. 
        /// </summary>
        /// <returns>A list of SqlParameters.</returns>
        protected SqlParameter[] CreateUpdateParameters(ADataObject instance)
        {
            return CreateInsertOrUpdateParameters(instance, false);
        }
        
        /// <summary>
        /// Creates parameters for an INSERT or an UPDATE database operation. 
        /// </summary>
        /// <returns>A list of SqlParameters.</returns>
        protected virtual SqlParameter[] CreateInsertOrUpdateParameters(ADataObject instance, bool insert)
        {
            var paramList = new List<SqlParameter>();

            foreach (var column in instance.DatabaseColumns)
            {
                // Get the instance of this column attribute.
                var attribute = ADataObject.GetDbColumnAttribute(column);

                // Skip Id attributes on insert and read only attributes.
                if ((insert && attribute.IsId) || attribute.IsReadOnly || attribute.IsIgnored)
                {
                    continue;  
                }

                // Add parameter to the list of parameters.
                paramList.Add(new SqlParameter(GetParameterName(attribute.Name), column.GetValue(instance)));
            }

            return paramList.ToArray();
        }

        /// <summary>
        /// Creates parameters for an GET or an DELETE database operation. 
        /// </summary>
        /// <returns>A list of SqlParameters.</returns>
        protected virtual SqlParameter[] CreateIdParameters(ADataObject instance)
        {
            var paramList = new List<SqlParameter>();

            foreach (var column in instance.DatabaseColumns)
            {
                // Get the instance of this column attribute.
                var attribute = ADataObject.GetDbColumnAttribute(column);

                // Add Id attributes only.
                if (attribute.IsId)
                {
                    // Add parameter to the list of parameters.
                    paramList.Add(new SqlParameter(GetParameterName(attribute.Name), column.GetValue(instance)));
                }
            }

            return paramList.ToArray();
        }

        /// <summary>
        /// Creates parameters for an GET or an DELETE database operation. 
        /// </summary>
        /// <returns>A list of SqlParameters.</returns>
        protected virtual SqlParameter[] CreateIdParameters(int id)
        {
            if (_typeInstance is IId == false || _typeInstance.IsDatabaseTable == false)
            {
                throw new Exception("Can not create an Id parameter from a non IId or non database object.");
            }

            var paramList = new List<SqlParameter>();

            foreach (var attribute in _typeInstance.DatabaseColumns.Select(ADataObject.GetDbColumnAttribute).Where(attribute => attribute.IsId))
            {
                // Add parameter to the list of parameters.
                paramList.Add(new SqlParameter(GetParameterName(attribute.Name), id));    

                break;
            }

            return paramList.ToArray();
        }
        

        /// <summary>
        /// Returns a stored procedure name for an SELECT operation.
        /// </summary>
        /// <returns>A name of a stored procedure.</returns>
        protected virtual string GetSelectStoredProcedureName()
        {
            return StoredProcedureBaseName + "_SelectList";
        }

        /// <summary>
        /// Returns A stored procedure name for an SELECT by ID operation.
        /// </summary>
        /// <returns>A name of a stored procedure.</returns>
        protected virtual string GetSelectDetailsStoredProcedureName()
        {
            return StoredProcedureBaseName + "_SelectDetails";
        }

        /// <summary>
        /// Returns a stored procedure name for an UPDATE operation.
        /// </summary>
        /// <returns>A name of a stored procedure.</returns>
        protected virtual string GetUpdateStoredProcedureName()
        {
            return StoredProcedureBaseName + "_Update";
        }

        /// <summary>
        /// Returns a stored procedure name for an INSERT operation.
        /// </summary>
        /// <returns>A name of a stored procedure.</returns>
        protected virtual string GetInsertStoredProcedureName()
        {
            return StoredProcedureBaseName + "_Insert";
        }

        /// <summary>
        /// Returns stored procedure name for the DELETE operation.
        /// </summary>
        /// <returns>A name of a stored procedure.</returns>
        protected virtual string GetDeleteStoredProcedureName()
        {
            return StoredProcedureBaseName + "_Delete";
        }


        /// <summary>
        /// Creates a parameter name from a column name.
        /// </summary>
        /// <param name="columnName">A column name.</param>
        /// <returns>A parameter name.</returns>
        protected string GetParameterName(string columnName)
        {
            return "@" + columnName;
        }


        /// <summary>
        /// The SaveAll operation.
        /// </summary>
        /// <param name="transaction">A SQL transaction.</param>
        /// <param name="data">A list of object to be stored in the database.</param>
        private void SaveAllOperation(SqlTransaction transaction, object data)
        {
            var objects = (IEnumerable<T>)data;
            foreach (var obj in objects)
            {
                try
                {
                    Save(obj, transaction);
                }
                catch (Exception ex)
                {
                    var exception = new DatabaseException("Can not save a data item.", ex);

                    exception.Data.Add(obj.GetType().Name, obj.ToString());

                    throw exception;
                }
            }
        }

        #endregion
    }
}
