/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Core.Injektor
{
    using System;


    /// <summary>
    /// An instance of this type is already registered exception.
    /// </summary>
    public class InstanceAlreadyRegisteredException : Exception
    {
        public InstanceAlreadyRegisteredException(string message) 
            : base(message)
        {
        }


        public InstanceAlreadyRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
