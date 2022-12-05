/* (C) 2016 Premysl Fara */

namespace ArtMananager.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Media;

    using ArtMananager.Forms;


    public class ImagePreview
    {
        public string Path { get; set; }
        public ImageSource Image { get; set; }


        public static ObservableCollection<ImagePreview> LoadImages(string resourcesDir)
        {
            var imageFilesList = new List<string>();
                             
            try
            {
                var count = 0;
                var dirInfo = new DirectoryInfo(resourcesDir);
                foreach (var file in dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                {
                    var fn = file.Name.ToLower();

                    // Only images.
                    if (fn.EndsWith(".jpg") || fn.EndsWith(".jpeg"))
                    {
                        imageFilesList.Add(file.FullName);

                        count++;
                        if (count >= 20)
                        {
                            // No more than 20 images in preview.
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ;  // Exceptions can be ignored.
            }

            var results = new ObservableCollection<ImagePreview>();

            // No images found?
            if (imageFilesList.Count == 0)
            {
                return results;
            }

            foreach (var fileName in imageFilesList)
            {
                results.Add(new ImagePreview() { Path = fileName, Image = UIHelper.LoadImage(fileName) });
            }

            return results;
        }
    }
}
