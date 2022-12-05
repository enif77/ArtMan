/* (C) 2016 Premysl Fara */

namespace CvsDbTest.DataObjects
{
    using System;

    using CsvDb;
    using CvsDbTest.Core;


    [DbTable("Autor")]
    public class Autor : AIdDataObject, ILookup, IUpdatable<Autor>
    {
        #region public fields

        public static readonly Autor RequiredValue = EmptyValue<Autor>.Required;
        public static readonly Autor OptionalValue = EmptyValue<Autor>.Optional;

        #endregion


        #region fields

        private int _id;
        private string _name;
        private string _jmeno;
        private string _prijmeni;
        private string _webUrl;
        private string _wikipediaUrl;
        private string _telefon;
        private string _adresa;
        private string _email;
        private string _popis;
        private string _resourcesDir;

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
            Popis = String.Empty;
            ResourcesDir = String.Empty;
        }

        #endregion


        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get { return Popis; }
            set { Popis = value; }
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
        public string Popis
        {
            get { return _popis; }
            set
            {
                if (_popis != value)
                {
                    _popis = value;
                    OnPropertyChanged("Popis");
                    OnPropertyChanged("Description");
                }
            }
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


        public override string ToString()
        {
            return Name;
        }


        private void Updatename()
        {
            _name = String.Format(
                        "{0} {1}",
                        _jmeno ?? String.Empty,
                        _prijmeni ?? String.Empty);

            OnPropertyChanged("Name");
        }


        public bool NeedsUpdate(Autor source)
        {
            if (Id != source.Id) return true;
            if (Jmeno != source.Jmeno) return true;
            if (Prijmeni != source.Prijmeni) return true;
            if (WebUrl != source.WebUrl) return true;
            if (Telefon != source.Telefon) return true;
            if (Adresa != source.Adresa) return true;
            if (Email != source.Email) return true;
            if (Popis != source.Popis) return true;
            if (WikipediaUrl != source.WikipediaUrl) return true;
            if (ResourcesDir != source.ResourcesDir) return true;

            return false;
        }


        public void Update(Autor source)
        {
            throw new NotImplementedException();
        }
    }
}
