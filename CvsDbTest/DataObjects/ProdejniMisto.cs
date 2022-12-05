/* (C) 2016 Premysl Fara */

namespace CvsDbTest.DataObjects
{
    using System;

    using CsvDb;
    using CvsDbTest.Core;


    [DbTable("ProdejniMisto")]
    public class ProdejniMisto : AIdDataObject, ILookup, IUpdatable<ProdejniMisto>
    {
        #region public fields

        public static readonly ProdejniMisto RequiredValue = EmptyValue<ProdejniMisto>.Required;
        public static readonly ProdejniMisto OptionalValue = EmptyValue<ProdejniMisto>.Optional;

        #endregion


        #region fields

        private int _id;
        private string _nazev;
        private string _popis;
        private string _telefon;
        private string _adresa;
        private string _email;
        private string _webUrl;
        private string _poznamka;

        #endregion


        #region ctor

        public ProdejniMisto()
        {
            Popis = String.Empty;
            Telefon = String.Empty;
            Adresa = String.Empty;
            Email = String.Empty;
            WebUrl = String.Empty;
            Poznamka = String.Empty;
        }

        #endregion


        public string Name
        {
            get { return Nazev; }
            set { Nazev = value; }
        }

        public string Description
        {
            get { return Popis; }
            set { Popis = value; }
        }


        [DbColumn("Nazev", Int32.MaxValue)]
        public string Nazev
        {
            get { return _nazev; }
            set
            {
                if (_nazev != value)
                {
                    _nazev = value;
                    OnPropertyChanged("Nazev");
                    OnPropertyChanged("Name");
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
         
        [DbColumn("Poznamka", Int32.MaxValue)]
        public string Poznamka
        {
            get { return _poznamka; }
            set
            {
                if (_poznamka != value)
                {
                    _poznamka = value;
                    OnPropertyChanged("Poznamka");
                }
            }
        }


        public override string ToString()
        {
            return Name;
        }


        public bool NeedsUpdate(ProdejniMisto source)
        {
            if (Id != source.Id) return true;
            if (Nazev != source.Nazev) return true;
            if (Popis != source.Popis) return true;
            if (Telefon != source.Telefon) return true;
            if (Adresa != source.Adresa) return true;
            if (Email != source.Email) return true;
            if (WebUrl != source.WebUrl) return true;
            if (Poznamka != source.Poznamka) return true;

            return false;
        }


        public void Update(ProdejniMisto source)
        {
            throw new NotImplementedException();
        }
    }
}
