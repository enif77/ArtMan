/* (C) 2016 Premysl Fara */

namespace ArtMan.DataObjects
{
    using System;

    using ArtMan.Core.Data;


    [DbTable("Majitel")]
    public class Majitel : AIdDataObject, ILookup, IUpdatable<Majitel>
    {
        #region public fields

        public static readonly Majitel RequiredValue = EmptyValue<Majitel>.Required;
        public static readonly Majitel OptionalValue = EmptyValue<Majitel>.Optional;

        #endregion


        #region fields

        private string _name;
        private string _jmeno;
        private string _prijmeni;
        private string _telefon;
        private string _adresa;
        private string _email;
        private string _poznamka;

        #endregion


        #region ctor

        public Majitel()
        {
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
            get { return Poznamka; }
            set { Poznamka = value; }
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

        [DbColumn("Poznamka", Int32.MaxValue)]
        string Poznamka
        {
            get { return _poznamka; }
            set
            {
                if (_poznamka != value)
                {
                    _poznamka = value;
                    OnPropertyChanged("Poznamka");
                    OnPropertyChanged("Description");
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


        public bool NeedsUpdate(Majitel source)
        {
            if (Id != source.Id) return true;
            if (Jmeno != source.Jmeno) return true;
            if (Prijmeni != source.Prijmeni) return true;
            if (Telefon != source.Telefon) return true;
            if (Adresa != source.Adresa) return true;
            if (Email != source.Email) return true;
            if (Poznamka != source.Poznamka) return true;

            return false;
        }


        public void Update(Majitel source)
        {
            throw new NotImplementedException();
        }
    }
}
