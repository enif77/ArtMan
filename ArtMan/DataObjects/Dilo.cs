/* (C) 2016 Premysl Fara */

namespace ArtMan.DataObjects
{
    using System;

    using ArtMan.Core.Data;


    [DbTable("Dilo")]
    public class Dilo : AIdDataObject, ILookup, IUpdatable<Dilo>
    {
        #region public fields

        public static readonly Dilo RequiredValue = EmptyValue<Dilo>.Required;
        public static readonly Dilo OptionalValue = EmptyValue<Dilo>.Optional;

        #endregion


        #region fields

        private DateTime _koupenoKdy;
        private decimal _nakupCena;
        private int _nakupMenaId;
        private int _koupenoKdeId;

        private DateTime? _prodanoKdy;
        private decimal _prodejCena;
        private int _prodejMenaId;
        private int _prodanoKdeId;

        //private int _majitel1Id;
        //private int _majitel2Id;

        private int _autorId;
        private string _nazev;
        private int _rok;
        private int _technikaId;
        private string _rozmer;
        private int _typDilaId;

        private bool _skladem;
        private int _umisteniId;

        private bool _prodano;

        private string _wikipediaUrl;
        private string _resourcesDir;
        
        #endregion


        #region ctor

        public Dilo()
        {  
            Nazev = String.Empty;
            Rozmer = String.Empty;
            WikipediaUrl = String.Empty;
            ResourcesDir = String.Empty; 

            KoupenoKdy = DateTime.Today;
            ProdanoKdy = DateTime.Today;
        }

        #endregion

         
        public string Name
        {
            get { return Nazev; }
            set { Nazev = value; }
        }

        public string Description
        {
            get { return Nazev; }
            set { Nazev = value; }
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
                    OnPropertyChanged("NakupCena");
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
                    OnPropertyChanged("Description");
                }
            }
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

        [DbColumn("Rozmer")]
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

        //[DbColumn("Majitel1Id")]
        //public int Majitel1Id
        //{
        //    get { return _majitel1Id; }
        //    set
        //    {
        //        if (_majitel1Id != value)
        //        {
        //            _majitel1Id = value;
        //            OnPropertyChanged("Majitel1Id");
        //        }
        //    }
        //}

        //[DbColumn("Majitel2Id")]
        //public int Majitel2Id
        //{
        //    get { return _majitel2Id; }
        //    set
        //    {
        //        if (_majitel2Id != value)
        //        {
        //            _majitel2Id = value;
        //            OnPropertyChanged("Majitel2Id");
        //        }
        //    }
        //}

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


        public bool NeedsUpdate(Dilo source)
        {
            if (Id != source.Id) return true;
            if (Nazev != source.Nazev) return true;
            if (KoupenoKdy != source.KoupenoKdy) return true;
            if (ProdanoKdy != source.ProdanoKdy) return true;
            if (NakupCena != source.NakupCena) return true;
            if (NakupMenaId != source.NakupMenaId) return true;
            if (ProdejCena != source.ProdejCena) return true;
            if (ProdejMenaId != source.ProdejMenaId) return true;
            if (AutorId != source.AutorId) return true;
            if (Rok != source.Rok) return true;
            if (TechnikaId != source.TechnikaId) return true;
            if (Rozmer != source.Rozmer) return true;
            if (Skladem != source.Skladem) return true;
            if (Prodano != source.Prodano) return true;
            //if (Majitel1Id != source.Majitel1Id) return true;
            //if (Majitel2Id != source.Majitel2Id) return true;
            if (KoupenoKdeId != source.KoupenoKdeId) return true;
            if (ProdanoKdeId != source.ProdanoKdeId) return true;
            if (TypDilaId != source.TypDilaId) return true;
            if (UmisteniId != source.UmisteniId) return true;
            if (WikipediaUrl != source.WikipediaUrl) return true;
            if (ResourcesDir != source.ResourcesDir) return true;

            return false;
        }


        public void Update(Dilo source)
        {
            throw new NotImplementedException();
        }
    }
}
