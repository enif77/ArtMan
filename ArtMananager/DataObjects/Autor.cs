/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("Autor")]
    public class Autor : ALookupDataObject<Autor>
    {
        #region fields

        private string _jmeno;
        private string _prijmeni;
        private string _webUrl;
        private string _wikipediaUrl;
        private string _telefon;
        private string _adresa;
        private string _email;
        private string _resourcesDir;
        private string _thumbnailName;

        #endregion


        #region ctor

        public Autor()
        {
            Jmeno = String.Empty;
            Prijmeni = String.Empty;
            WebUrl = String.Empty;
            WikipediaUrl = String.Empty;
            Telefon = String.Empty;
            Adresa = String.Empty;
            Email = String.Empty;
            Description = String.Empty;
            ResourcesDir = String.Empty;
            ThumbnailName = String.Empty;
        }

        #endregion


        [DbColumn("Name", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Ignored)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
         
        [DbColumn("Jmeno", Int32.MaxValue)]
        public string Jmeno
        {
            get { return _jmeno; }
            set
            {
                if (_jmeno != value)
                {
                    _jmeno = value;
                    OnPropertyChanged("Jmeno");

                    Updatename();
                }
            }
        }

        [DbColumn("Prijmeni", Int32.MaxValue)]
        public string Prijmeni
        {
            get { return _prijmeni; }
            set
            {
                if (_prijmeni != value)
                {
                    _prijmeni = value;
                    OnPropertyChanged("Prijmeni");

                    Updatename();
                }
            }
        }

        [DbColumn("WebURL", Int32.MaxValue)]
        public string WebUrl
        {
            get { return _webUrl; }
            set
            {
                if (_webUrl != value)
                {
                    _webUrl = value;
                    OnPropertyChanged("WebUrl");
                }
            }
        }

        [DbColumn("WikipediaURL", Int32.MaxValue)]
        public string WikipediaUrl
        {
            get { return _wikipediaUrl; }
            set
            {
                if (_wikipediaUrl != value)
                {
                    _wikipediaUrl = value;
                    OnPropertyChanged("WikipediaUrl");
                }
            }
        }

        [DbColumn("Telefon", Int32.MaxValue)]
        public string Telefon
        {
            get { return _telefon; }
            set
            {
                if (_telefon != value)
                {
                    _telefon = value;
                    OnPropertyChanged("Telefon");
                }
            }
        }

        [DbColumn("Adresa", Int32.MaxValue)]
        public string Adresa
        {
            get { return _adresa; }
            set
            {
                if (_adresa != value)
                {
                    _adresa = value;
                    OnPropertyChanged("Adresa");
                }
            }
        }

        [DbColumn("Email", Int32.MaxValue)]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        
        [DbColumn("Popis", Int32.MaxValue)]
        public sealed override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }

        /// <summary>
        /// Adresar s dokumentaci.
        /// </summary>
        [DbColumn("ResourcesDir", Int32.MaxValue)]
        public string ResourcesDir
        {
            get { return _resourcesDir; }
            set
            {
                if (_resourcesDir != value)
                {
                    _resourcesDir = value;
                    OnPropertyChanged("ResourcesDir");
                }
            }
        }

        /// <summary>
        /// Soubor s nahledem.
        /// </summary>
        [DbColumn("ThumbnailName", Int32.MaxValue)]
        public string ThumbnailName
        {
            get { return _thumbnailName; }
            set
            {
                if (_thumbnailName != value)
                {
                    _thumbnailName = value;
                    OnPropertyChanged("ThumbnailName");
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }


        private void Updatename()
        {
            Name = String.Format("{0} {1}", 
                Prijmeni ?? String.Empty,
                Jmeno ?? String.Empty);
        }


        public override bool NeedsUpdate(Autor source)
        {
            if (Jmeno != source.Jmeno) return true;
            if (Prijmeni != source.Prijmeni) return true;
            if (WebUrl != source.WebUrl) return true;
            if (Telefon != source.Telefon) return true;
            if (Adresa != source.Adresa) return true;
            if (Email != source.Email) return true;
            if (WikipediaUrl != source.WikipediaUrl) return true;
            if (ResourcesDir != source.ResourcesDir) return true;
            if (ThumbnailName != source.ThumbnailName) return true;

            return base.NeedsUpdate(source);
        }
    }
}
