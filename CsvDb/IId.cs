/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    /// <summary>
    /// An interface for identifiable objects.
    /// </summary>
    public interface IId
    {
        /// <summary>
        /// An instance ID.
        /// </summary>
        int Id { get; set; }
    }
}
