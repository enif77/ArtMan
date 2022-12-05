/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms.Controls
{
    public enum EzpDatePickerMode
    {
        /// <summary>
        /// Mandatory checkbox is not visible (in principal equals to checked checkbox, thus date has to be selected)
        /// </summary>
        Mandatory,

        /// <summary>
        /// Unchecked mandatory checkbox and unselected date means DateTime.MaxValue
        /// </summary>
        Minimum,

        /// <summary>
        /// Unchecked mandatory checkbox and unselected date means DateTime.MinValue
        /// </summary>
        Maximum,
    }
}
