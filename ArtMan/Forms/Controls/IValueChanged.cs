/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms.Controls
{
    public interface IValueChanged
    {
        /// <summary>
        /// Returns true, if the current value differs from the original value.
        /// </summary>
        bool IsValueChanged { get; }

        /// <summary>
        /// Original value from DB/data source.
        /// </summary>
        object OriginalValue { get; set; }
    }
}
