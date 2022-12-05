/* (C) 2016 Premysl Fara */

namespace ArtMananager.Core
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;


    public class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }


        public static void OpenFile(Item item)
        {
            OpenFile(item.Path);
        }


        public static void OpenFile(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception)
            {
                ;  // Can fail silently.
            }
        }
    }


    public class FileItem : Item
    {

    }


    public class DirectoryItem : Item
    {
        public ObservableCollection<Item> Items { get; set; }


        public DirectoryItem()
        {
            Items = new ObservableCollection<Item>();
        }
    }


    public class ItemProvider
    {
        public static ObservableCollection<Item> GetItems(string path)
        {
            var items = new ObservableCollection<Item>();

            try
            {
                var dirInfo = new DirectoryInfo(path);

                foreach (var directory in dirInfo.GetDirectories())
                {
                    var item = new DirectoryItem
                    {
                        Name = directory.Name,
                        Path = directory.FullName,
                        Items = GetItems(directory.FullName)
                    };

                    items.Add(item);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    var item = new FileItem
                    {
                        Name = file.Name,
                        Path = file.FullName
                    };

                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                var item = new FileItem
                {
                    Name = ex.Message,
                    Path = ex.ToString()
                };

                items.Add(item);
            }

            return items;
        }
    }
}
