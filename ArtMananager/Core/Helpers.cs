/* (C) 2017 Premysl Fara */

namespace ArtMananager.Core
{
    using System;
    using System.IO;


    public static class Helpers
    {
        /// <summary>
        /// Returns a temporary file name.
        /// </summary>
        /// <param name="extension">A file extension or null.</param>
        /// <returns>A temporary file name.</returns>
        public static string GetTempFileName(string extension)
        {
            var attempt = 0;
            while (true)
            {
                var fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, extension);
                fileName = Path.Combine(Path.GetTempPath(), fileName);

                try
                {
                    using (new FileStream(fileName, FileMode.CreateNew)) { }

                    return fileName;
                }
                catch (IOException ex)
                {
                    if (++attempt == 10)
                    {
                        throw new IOException("No unique temporary file name is available.", ex);
                    }
                }
            }
        }
         
        /// <summary>
        /// GetStringInBetween finds the first occurrence of the “begin” and “end” strings, 
        /// then you can use result[1] to allow you to move ahead down the html document to 
        /// find the next value.
        /// </summary>
        /// <param name="startString">A start string.</param>
        /// <param name="endString">An end string.</param>
        /// <param name="sourceString">A source string.</param>
        /// <param name="includeStartString">Should the result include the start string.</param>
        /// <param name="includeEndString">Should the result contain the end string.</param>
        /// <returns>Result[0] = found string or null. Result[1] = where to search next or null.</returns>
        public static string[] GetStringBetween(
            string startString, string endString, string sourceString,
            bool includeStartString = false, bool includeEndString = false)
        {
            if (String.IsNullOrEmpty(startString))
            {
                throw new ArgumentException("A start string expected.", "startString");
            }

            if (String.IsNullOrEmpty(endString))
            {
                throw new ArgumentException("An end string expected.", "endString");
            }

            // Prepare an empty result.
            string[] result = { null, null };

            // Find the first occurence of the start string.
            var indexOfStart = sourceString.IndexOf(startString, StringComparison.InvariantCulture);
            if (indexOfStart == -1)
            {
                // Stay where we are.
                result[1] = sourceString;

                // No start found. The result[0] remains null.
                return result;
            }

            // Include the Begin string if desired.
            if (includeStartString)
            {
                indexOfStart -= startString.Length;
            }

            // Create a new sourceString, cutting out the start string.
            // TODO: This should be avoided - set the "start looking for end from" variable instead.
            sourceString = sourceString.Substring(indexOfStart + startString.Length);

            // Find the first occurence of the end string after the start string.
            var indexOfEnd = sourceString.IndexOf(endString, StringComparison.InvariantCulture);
            if (indexOfEnd == -1)
            {
                // The end string can was not found. 
                // The result[0] remains null.
                // The result[1] remains null = there is nowhere to go next.
                return result;
            }

            // include the End string if desired
            if (includeEndString)
            {
                indexOfEnd += endString.Length;
            }

            // Set the result[0] to the string between the start and the end strings.
            result[0] = sourceString.Substring(0, indexOfEnd);

            // Advance beyond this segment.
            if (indexOfEnd + endString.Length < sourceString.Length)
            {
                result[1] = sourceString.Substring(indexOfEnd + endString.Length);
            }

            return result;
        }
    }
}
