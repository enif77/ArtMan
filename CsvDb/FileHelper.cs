/* (C) 2016 Přemysl Fára */

namespace CsvDb
{
    using System;
    using System.Collections.Generic;
    using System.IO;


    /// <summary>
    /// File operations.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Loads a file into an array of strings. Each line as a separate string.
        /// </summary>
        /// <param name="fileName">A file name.</param>
        /// <param name="trimNonEmpty">Trims all nonempty lines if true.</param>
        /// <returns>An array of strings or exception.</returns>
        public static string[] LoadDataAsArrayOfStrings(string fileName, bool trimNonEmpty = false)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("A file name expected.", "fileName");
            }

            var data = File.ReadAllLines(fileName);

            // Trim all nonempty lines.
            if (trimNonEmpty)
            {
                for (var i = 0; i < data.Length; i++)
                {
                    if (String.IsNullOrEmpty(data[i]) == false)
                    {
                        data[i] = data[i].Trim();
                    }
                }
            }

            return data;
        }
    }
}
