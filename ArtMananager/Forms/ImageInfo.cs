/* (C) 2017 Premysl Fara */

namespace ArtMananager.Forms
{
    public class ImageInfo
    {
        public string Path { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Ratio
        {
            get
            {
                return (Height <= 0) ? 1 : Width / Height;
            }
        }
    }
}
