/* (C) 2016 - 2017 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("Configuration")]
    public class Configuration : AIdDataObject
    {
        #region constans

        /// <summary>
        /// The default if the main app. window was maximized.
        /// </summary>
        public const bool DefaultMaximized = false;

        /// <summary>
        /// The default position of the top endge of the main app. window.
        /// </summary>
        public const int DefaultTop = 0;

        /// <summary>
        /// The default position of the left endge of the main app. window.
        /// </summary>
        public const int DefaultLeft = 0;

        /// <summary>
        /// The default width of the main app. window.
        /// </summary>
        public const int DefaultWidth = 1024;

        /// <summary>
        /// The default height of the main app. window.
        /// </summary>
        public const int DefaultHeight = 768;


        /// <summary>
        /// The default preview image size.
        /// </summary>
        public const int DefaultPreviewImageSize = 100;

        #endregion


        #region fields

        private bool _isMaximized;
        private int _top;
        private int _left;
        private int _width;
        private int _height;

        private string _baseResourcesDirectoryPath;
        private int _previewImageSize;

        #endregion


        #region ctor

        public Configuration() 
        {
            IsMaximized = DefaultMaximized;
            Top = DefaultTop;
            Left = DefaultLeft;
            Width = DefaultWidth;
            Height = DefaultHeight;

            BaseResourcesDirectoryPath = String.Empty;
            PreviewImageSize = DefaultPreviewImageSize;
        }

        #endregion


        /// <summary>
        /// Gets or sets if the main app. window was maximized.
        /// </summary>
        [DbColumn("IsMaximized")]
        public bool IsMaximized
        {
            get { return _isMaximized; }
            set
            {
                if (_isMaximized != value)
                {
                    _isMaximized = value;
                    OnPropertyChanged("IsMaximized");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the top edge of the main app. window.
        /// </summary>
        [DbColumn("Top")]
        public int Top
        {
            get { return _top; }
            set
            {
                if (_top != value)
                {
                    _top = value;
                    OnPropertyChanged("Top");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the left edge of the main app. window.
        /// </summary>
        [DbColumn("Left")]
        public int Left
        {
            get { return _left; }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    OnPropertyChanged("Left");
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the main app. window.
        /// </summary>
        [DbColumn("Width")]
        public int Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the main app. window.
        /// </summary>
        [DbColumn("Height")]
        public int Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }


        /// <summary>
        /// Gets or sets the preview image size.
        /// </summary>
        [DbColumn("PreviewImageSize")]
        public int PreviewImageSize
        {
            get { return _previewImageSize; }
            set
            {
                if (_previewImageSize != value)
                {
                    _previewImageSize = value;
                    OnPropertyChanged("PreviewImageSize");
                }
            }
        }

        /// <summary>
        /// Gets or sets the base of resources directory path.
        /// </summary>
        [DbColumn("BaseResourcesDirectoryPath")]
        public string BaseResourcesDirectoryPath
        {
            get { return _baseResourcesDirectoryPath; }
            set
            {
                if (_baseResourcesDirectoryPath != value)
                {
                    _baseResourcesDirectoryPath = value;
                    OnPropertyChanged("BaseResourcesDirectoryPath");
                }
            }
        }
    }
}
