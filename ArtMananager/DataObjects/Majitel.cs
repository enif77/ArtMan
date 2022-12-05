/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("Majitel")]
    public sealed class Majitel : ALookupDataObject<Majitel>
    {
        #region fields

        private string _jmeno;
        private string _prijmeni;
        private string _telefon;
        private string _adresa;
        private string _email;

        #endregion


        #region ctor

        public Majitel()
        {
            Prijmeni = String.Empty;
            Telefon = String.Empty;
            Adresa = String.Empty;
            Email = String.Empty;
            Description = String.Empty;
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
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
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


        public override bool NeedsUpdate(Majitel source)
        {
            if (Jmeno != source.Jmeno) return true;
            if (Prijmeni != source.Prijmeni) return true;
            if (Telefon != source.Telefon) return true;
            if (Adresa != source.Adresa) return true;
            if (Email != source.Email) return true;

            return base.NeedsUpdate(source);
        }
    }
}
