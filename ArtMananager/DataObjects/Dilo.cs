/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("Dilo")]
    public sealed class Dilo : ALookupDataObject<Dilo>
    {
        #region fields

        private bool _useKoupenoKdyFullDate;
        private DateTime _koupenoKdy;
        private decimal _nakupCena;
        private decimal _nakupCenaKurz;
        private decimal _nakupCenaCzk;
        private decimal _dnesniCenaCzk;
        private int _nakupMenaId;
        private int _koupenoKdeId;

        private DateTime? _prodanoKdy;
        private decimal _prodejCena;
        private int _prodejMenaId;
        private int _prodanoKdeId;

        private int _majitelId;

        private int _autorId;
        private int _rok;
        private int _technikaId;
        private string _rozmer;
        private int _typDilaId;

        private bool _skladem;
        private int _umisteniId;

        private bool _pojisteno;
        private bool _prodano;

        private string _wikipediaUrl;
        private string _resourcesDir;
        private string _thumbnailName;

        private int _kusu;
        
        #endregion


        #region ctor

        public Dilo()
        {  
            Rozmer = String.Empty;
            WikipediaUrl = String.Empty;
            ResourcesDir = String.Empty; 
            ThumbnailName = String.Empty;
            
            KoupenoKdy = DateTime.Today;
            ProdanoKdy = DateTime.Today;

            Kusu = 1;
        }

        #endregion

        [DbColumn("UseKoupenoKdyFullDate")]
        public bool UseKoupenoKdyFullDate
        {
            get { return _useKoupenoKdyFullDate; }
            set
            {
                if (_useKoupenoKdyFullDate != value)
                {
                    _useKoupenoKdyFullDate = value;
                    OnPropertyChanged("UseKoupenoKdyFullDate");
                }
            }
        }

        [DbColumn("Koupeno")]
        public DateTime KoupenoKdy
        {
            get { return _koupenoKdy; }
            set
            {
                if (_koupenoKdy != value)
                {
                    _koupenoKdy = value;
                    OnPropertyChanged("KoupenoKdy");
                    OnPropertyChanged("KoupenoKdyRok");
                }
            }
        }

        public int KoupenoKdyRok
        {
            get { return _koupenoKdy.Year; }
            set
            {
                if (_koupenoKdy.Year != value)
                {
                    KoupenoKdy = new DateTime(value, _koupenoKdy.Month, _koupenoKdy.Day);
                }
            }
        }

        [DbColumn("Prodano")]
        public DateTime? ProdanoKdy
        {
            get { return _prodanoKdy; }
            set
            {
                if (_prodanoKdy != value)
                {
                    _prodanoKdy = value;
                    OnPropertyChanged("ProdanoKdy");
                }
            }
        }

        [DbColumn("NakupCena")]
        public decimal NakupCena
        {
            get { return _nakupCena; }
            set
            {
                if (_nakupCena != value)
                {
                    _nakupCena = value;

                    NakupCenaCzk = _nakupCena * _nakupCenaKurz;

                    OnPropertyChanged("NakupCena");
                }
            }
        }

        [DbColumn("NakupCenaKurz")]
        public decimal NakupCenaKurz
        {
            get { return _nakupCenaKurz; }
            set
            {
                if (_nakupCenaKurz != value)
                {
                    _nakupCenaKurz = value;

                    NakupCenaCzk = _nakupCena * _nakupCenaKurz;

                    OnPropertyChanged("NakupCenaKurz");
                }
            }
        }

        [DbColumn("NakupCenaCzk")]
        public decimal NakupCenaCzk
        {
            get { return _nakupCenaCzk; }
            set
            {
                if (_nakupCenaCzk != value)
                {
                    _nakupCenaCzk = value;
                    OnPropertyChanged("NakupCenaCzk");
                }
            }
        }

        [DbColumn("DnesniCenaCzk")]
        public decimal DnesniCenaCzk
        {
            get { return _dnesniCenaCzk; }
            set
            {
                if (_dnesniCenaCzk != value)
                {
                    _dnesniCenaCzk = value;
                    OnPropertyChanged("DnesniCenaCzk");
                }
            }
        }

        [DbColumn("NakupMenaId")]
        public int NakupMenaId
        {
            get { return _nakupMenaId; }
            set
            {
                if (_nakupMenaId != value)
                {
                    _nakupMenaId = value;
                    OnPropertyChanged("NakupMenaId");
                }
            }
        }

        [DbColumn("ProdejCena")]
        public decimal ProdejCena
        {
            get { return _prodejCena; }
            set
            {
                if (_prodejCena != value)
                {
                    _prodejCena = value;
                    OnPropertyChanged("ProdejCena");
                }
            }
        }

        [DbColumn("ProdejMenaId")]
        public int ProdejMenaId
        {
            get { return _prodejMenaId; }
            set
            {
                if (_prodejMenaId != value)
                {
                    _prodejMenaId = value;
                    OnPropertyChanged("ProdejMenaId");
                }
            }
        }

        [DbColumn("AutorId")]
        public int AutorId
        {
            get { return _autorId; }
            set
            {
                if (_autorId != value)
                {
                    _autorId = value;
                    OnPropertyChanged("AutorId");
                }
            }
        }

        [DbColumn("Nazev", Int32.MaxValue)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [DbColumn("Rok")]
        public int Rok
        {
            get { return _rok; }
            set
            {
                if (_rok != value)
                {
                    _rok = value;
                    OnPropertyChanged("Rok");
                }
            }
        }

        [DbColumn("TechnikaId")]
        public int TechnikaId
        {
            get { return _technikaId; }
            set
            {
                if (_technikaId != value)
                {
                    _technikaId = value;
                    OnPropertyChanged("TechnikaId");
                }
            }
        }

        [DbColumn("Rozmer", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string Rozmer
        {
            get { return _rozmer; }
            set
            {
                if (_rozmer != value)
                {
                    _rozmer = value;
                    OnPropertyChanged("Rozmer");
                }
            }
        }

        [DbColumn("Skladem")]
        public bool Skladem
        {
            get { return _skladem; }
            set
            {
                if (_skladem != value)
                {
                    _skladem = value;
                    OnPropertyChanged("Skladem");
                }
            }
        }

        [DbColumn("Pojisteno")]
        public bool Pojisteno
        {
            get { return _pojisteno; }
            set
            {
                if (_pojisteno != value)
                {
                    _pojisteno = value;
                    OnPropertyChanged("Pojisteno");
                }
            }
        }

        [DbColumn("JeProdano")]
        public bool Prodano
        {
            get { return _prodano; }
            set
            {
                if (_prodano != value)
                {
                    _prodano = value;
                    OnPropertyChanged("Prodano");
                }
            }
        }

        [DbColumn("MajitelId")]
        public int MajitelId
        {
            get { return _majitelId; }
            set
            {
                if (_majitelId != value)
                {
                    _majitelId = value;
                    OnPropertyChanged("MajitelId");
                }
            }
        }

        [DbColumn("KoupenoKdeId")]
        public int KoupenoKdeId
        {
            get { return _koupenoKdeId; }
            set
            {
                if (_koupenoKdeId != value)
                {
                    _koupenoKdeId = value;
                    OnPropertyChanged("KoupenoKdeId");
                }
            }
        }

        [DbColumn("ProdanoKdeId")]
        public int ProdanoKdeId
        {
            get { return _prodanoKdeId; }
            set
            {
                if (_prodanoKdeId != value)
                {
                    _prodanoKdeId = value;
                    OnPropertyChanged("ProdanoKdeId");
                }
            }
        }

        [DbColumn("TypDilaId")]
        public int TypDilaId
        {
            get { return _typDilaId; }
            set
            {
                if (_typDilaId != value)
                {
                    _typDilaId = value;
                    OnPropertyChanged("TypDilaId");
                }
            }
        }

        [DbColumn("UmisteniId")]
        public int UmisteniId
        {
            get { return _umisteniId; }
            set
            {
                if (_umisteniId != value)
                {
                    _umisteniId = value;
                    OnPropertyChanged("UmisteniId");
                }
            }
        }
          
        [DbColumn("WikipediaURL", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
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

        /// <summary>
        /// Adresar s dokumentaci.
        /// </summary>
        [DbColumn("ResourcesDir", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
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
        [DbColumn("ThumbnailName", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
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

        [DbColumn("Popis", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }

        [DbColumn("Kusu")]
        public int Kusu
        {
            get { return _kusu; }
            set
            {
                if (_kusu != value)
                {
                    _kusu = value;
                    OnPropertyChanged("Kusu");
                }
            }
        }


        public override bool NeedsUpdate(Dilo source)
        {
            if (Id != source.Id) return true;

            if (UseKoupenoKdyFullDate != source.UseKoupenoKdyFullDate) return true;

            if (UseKoupenoKdyFullDate)
            {
                if (KoupenoKdy != source.KoupenoKdy) return true;
            }
            else
            {
                if (KoupenoKdyRok != source.KoupenoKdyRok) return true;
            }

            if (ProdanoKdy != source.ProdanoKdy) return true;
            if (NakupCena != source.NakupCena) return true;
            if (NakupCenaKurz != source.NakupCenaKurz) return true;
            if (NakupCenaCzk != source.NakupCenaCzk) return true;
            if (DnesniCenaCzk != source.DnesniCenaCzk) return true;
            if (NakupMenaId != source.NakupMenaId) return true;
            if (ProdejCena != source.ProdejCena) return true;
            if (ProdejMenaId != source.ProdejMenaId) return true;
            if (AutorId != source.AutorId) return true;
            if (Rok != source.Rok) return true;
            if (TechnikaId != source.TechnikaId) return true;
            if (Rozmer != source.Rozmer) return true;
            if (Skladem != source.Skladem) return true;
            if (Pojisteno != source.Pojisteno) return true;
            if (Prodano != source.Prodano) return true;
            if (MajitelId != source.MajitelId) return true;
            if (KoupenoKdeId != source.KoupenoKdeId) return true;
            if (ProdanoKdeId != source.ProdanoKdeId) return true;
            if (TypDilaId != source.TypDilaId) return true;
            if (UmisteniId != source.UmisteniId) return true;
            if (WikipediaUrl != source.WikipediaUrl) return true;
            if (ResourcesDir != source.ResourcesDir) return true;
            if (ThumbnailName != source.ThumbnailName) return true;
            if (Kusu != source.Kusu) return true;

            return base.NeedsUpdate(source);
        }
    }
}
