/* (C) 2016 Ezpada */

namespace ArtMan.Core.Data
{
    using System;


    /// <summary>
    /// A validation exception.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) 
            : base(message)
        {
        }


        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
