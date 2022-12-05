/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms
{
    using System.Windows.Controls;
    using System.Windows.Input;


    public static class DataGridHelper
    {
        /// <summary>
        /// Exports data from a DataGrid to CSV format stored in the clipboard.
        /// </summary> 
        /// <param name="grid">A DataGrid instance.</param>
        public static void CopyToClipboard(DataGrid grid)
        {
            var oldSelectionMode = grid.SelectionMode;
            var oldClipboardCopyMode = grid.ClipboardCopyMode;

            grid.SelectionMode = DataGridSelectionMode.Extended;
            grid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

            grid.SelectAllCells();

            ApplicationCommands.Copy.Execute(null, grid);

            grid.UnselectAllCells();

            grid.SelectionMode = oldSelectionMode;
            grid.ClipboardCopyMode = oldClipboardCopyMode;
        }
    }
}
