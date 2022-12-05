/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System;


    /// <summary>
    /// Base interface for an audited object.
    /// </summary>
    public interface IAuditedObject : IId
    {
        /// <summary>
        /// The last update date.
        /// </summary>
        DateTime UpdateDate { get; set; }

        /// <summary>
        /// The last use, who updated this object.
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// When was this object created.
        /// </summary>
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Who created this object.
        /// </summary>
        string CreatedBy { get; set; }
    }
}
