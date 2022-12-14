/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    /// <summary>
    /// An interface describing an lookup.
    /// </summary>
    public interface ILookup : IId
    {
        /// <summary>
        /// A name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A description.
        /// </summary>
        string Description { get; set; }
    }
}
