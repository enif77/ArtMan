/* (C) 2016 Premysl Fara */

namespace Injektor
{
    using System;


    /// <summary>
    /// An instance of this type is not registered exception.
    /// </summary>
    public class InstanceNotRegisteredException : Exception
    {
        public InstanceNotRegisteredException(string message) 
            : base(message)
        {
        }


        public InstanceNotRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
