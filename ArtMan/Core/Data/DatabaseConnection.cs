/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;


    public sealed class DatabaseConnection : IDisposable
    {
        #region properties

        /// <summary>
        /// Gets this connections connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
        }

        #endregion


        #region ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionString">A database connection string.</param>
        public DatabaseConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

            // TODO: pokud selže na deadlock, počkat 10s a zkusit znova.

            _connection.Open();
        }

        // Use C# destructor syntax for finalization code. 
        // This destructor will run only if the Dispose method 
        // does not get called. 
        // It gives your base class the opportunity to finalize. 
        // Do not provide destructors in types derived from this class.
        ~DatabaseConnection()
        {
            // Do not re-create Dispose clean-up code here. 
            // Calling Dispose(false) is optimal in terms of 
            // readability and maintainability.
            Dispose(false);
        }

        #endregion


        #region IDisposable

        // Resource.
        private readonly SqlConnection _connection = null;

        // Track whether Dispose has been called. 
        private bool _disposed = false;


        // Implement IDisposable. 
        // Do not make this method virtual. 
        // A derived class should not be able to override this method. 
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method. 
            // Therefore, you should call GC.SupressFinalize to 
            // take this object off the finalization queue 
            // and prevent finalization code for this object 
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios. 
        // If disposing equals true, the method has been called directly 
        // or indirectly by a user's code. Managed and unmanaged resources 
        // can be disposed. 
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed. 
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (_disposed == false)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if (disposing)
                {
                    // Dispose managed resources.
                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                        }

                        _connection.Dispose();
                    }
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here. 
                // If disposing is false, 
                // only the following code is executed.
                ;  // Nothing to do here. :-)

                // Note disposing has been done.
                _disposed = true;
            }
        }

        #endregion
    }
}
