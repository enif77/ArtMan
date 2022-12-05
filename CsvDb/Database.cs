/* (C) 2016 Přemys Fára */

namespace CsvDb
{
    using System;
    using System.IO;


    public class Database
    {
        public string RootDirectoryPath { get; private set; }


        public Database(string rootDirectoryPath)
        {
            if (String.IsNullOrEmpty(rootDirectoryPath))
            {
                throw new ArgumentNullException("rootDirectoryPath", "A root directory path expected.");
            }

            if (Directory.Exists(rootDirectoryPath) == false)
            {
                Directory.CreateDirectory(rootDirectoryPath);

                //throw new ArgumentException("rootDirectoryPath", String.Format("The root directory path '{0}' does not exists.", rootDirectoryPath));
            }

            RootDirectoryPath = rootDirectoryPath;
        }
    }
}
