/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System;


    /// <summary>
    /// An atribute that marks a property as a Name column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnTagAttribute : Attribute
    {
        /// <summary>
        /// A database column tag.
        /// </summary>
        public string Tag { get; set; }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="tag">A database column tag.</param>
        public DbColumnTagAttribute(string tag)
        {
            if (String.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("The tag argument expected.");
            }

            Tag = tag;
        }
    }
}
