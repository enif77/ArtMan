/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("ProdejniMisto")]
    public sealed class ProdejniMisto : ALookupDataObject<ProdejniMisto>
    {
        #region fields

        private string _telefon;
        private string _adresa;
        private string _email;
        private string _webUrl;
        private string _poznamka;

        #endregion


        #region ctor

        public ProdejniMisto()
        {
            Description = String.Empty;
            Telefon = String.Empty;
            Adresa = String.Empty;
            Email = String.Empty;
            WebUrl = String.Empty;
            Poznamka = String.Empty;
        }

        #endregion


        #region properties

        [DbColumn("Nazev", Int32.MaxValue)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [DbColumn("Popis", Int32.MaxValue)]
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
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

        #endregion



        public override bool NeedsUpdate(ProdejniMisto source)
        {
            if (Telefon != source.Telefon) return true;
            if (Adresa != source.Adresa) return true;
            if (Email != source.Email) return true;
            if (WebUrl != source.WebUrl) return true;
            if (Poznamka != source.Poznamka) return true;

            return base.NeedsUpdate(source);
        }


        public override void Update(ProdejniMisto source)
        {
            throw new NotImplementedException();
        }
    }
}
