/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;


    /// <summary>
    /// An atribute describing a database table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttribute : Attribute
    {
        /// <summary>
        /// A database table name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="name">A database table name.</param>
        public DbTableAttribute(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The name argument expected.");
            }

            Name = name;
        }
    }
}
