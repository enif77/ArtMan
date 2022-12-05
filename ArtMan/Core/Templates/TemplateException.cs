/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System;
    using System.Runtime.Serialization;


    /// <summary>
    /// Exception thrown when there are problems with template loading
    /// </summary>
    [Serializable]
    public class TemplateException : Exception
    {
        public TemplateException() { }
        public TemplateException(string message) : base(message) { }
        public TemplateException(string message, Exception inner) : base(message, inner) { }
        protected TemplateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
