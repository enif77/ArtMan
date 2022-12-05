/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;


    /// <summary>
    /// An attribute describing a DB column property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttribute : Attribute
    {
        /// <summary>
        /// Database column options.
        /// </summary>
        [Flags]
        public enum ColumnOptions
        {
            /// <summary>
            /// No flag set.
            /// </summary>
            None = 0,

            /// <summary>
            /// It is an ID column.
            /// </summary>
            Id = 1,          

            /// <summary>
            /// Can be null.
            /// </summary>
            Nullable = 2,

            /// <summary>
            /// Is read only. The value can not be changed.
            /// </summary>
            ReadOnly = 4,

            /// <summary>
            /// Is Ignored. The value is never read from/saved to DB.
            /// </summary>
            Ignored = 128,
        }

        /// <summary>
        /// A DB column name.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// A maximal allowed length of a string column.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Column options.
        /// </summary>
        public ColumnOptions Options { get; private set; }

        /// <summary>
        /// True, if this column allows a DbNull value.
        /// </summary>
        public bool IsNullable
        {
            get
            {
                return (Options & ColumnOptions.Nullable) == ColumnOptions.Nullable;
            }
        }
        
        /// <summary>
        /// True, if this column allows a DbNull value.
        /// </summary>
        public bool IsId
        {
            get
            {
                return (Options & ColumnOptions.Id) == ColumnOptions.Id;
            }
        }
        
        /// <summary>
        /// True, if this column is read only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return (Options & ColumnOptions.ReadOnly) == ColumnOptions.ReadOnly;
            }
        }

        /// <summary>
        /// True, if this column is ignored.
        /// </summary>
        public bool IsIgnored
        {
            get
            {
                return (Options & ColumnOptions.Ignored) == ColumnOptions.Ignored;
            }
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="name">A DB column name.</param>
        /// <param name="options">Column options.</param>
        public DbColumnAttribute(string name, ColumnOptions options = ColumnOptions.None)
            : this(name, Int32.MaxValue, options)
        {
        }
        
        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="name">A DB column name.</param>
        /// <param name="length">A maximal allowed length of a string column.</param>
        /// <param name="options">Column options.</param>
        public DbColumnAttribute(string name, int length, ColumnOptions options = ColumnOptions.None)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The name argument expected.");
            }

            Name = name;
            Length = length;
            Options = options;
        }
    }
}
