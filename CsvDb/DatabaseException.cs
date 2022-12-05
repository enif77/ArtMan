/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System;


    /// <summary>
    /// Exception which is thrown from a data layer.
    /// </summary>
    [Serializable]
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
            
        }


        public DatabaseException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}
