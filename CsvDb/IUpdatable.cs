/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    /// <summary>
    /// An interface, that allows an object to be updated.
    /// </summary>
    /// <typeparam name="T">A type.</typeparam>
    public interface IUpdatable<in T>
    {
        /// <summary>
        /// Checks, if this instance is the same as the source instance, or if it needs to be updated to match the source instance.
        /// </summary>
        /// <param name="source">A source instance.</param>
        /// <returns>True, if this instance needs to be updated.</returns>
        bool NeedsUpdate(T source);

        /// <summary>
        /// Updates this instance with values from the source instance.
        /// </summary>
        /// <param name="source">An instance with source data.</param>
        void Update(T source);
    }
}
