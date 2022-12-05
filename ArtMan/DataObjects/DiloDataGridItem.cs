/* (C) 2016 Premysl Fara */

namespace ArtMan.DataObjects
{
    using System;

    using ArtMan.Core.Data;


    [DbTable("Dilo")]
    public class DiloDataGridItem : AIdDataObject, IUpdatable<DiloDataGridItem>
    {
        #region fields

        private DateTime _koupeno;
        private decimal _nakupCena;
        private string _nakupMenaNazev;
        private string _koupenoKdeNazev;

        private bool _prodano;
        private DateTime _prodanoKdy;
        private decimal _prodejCena;
        private string _prodejMenaNazev;
        private string _prodanoKdeNazev;

        private string _autorJmeno;
        private string _autorPrijmeni;

        private string _nazev;
        private int _rok;
        private string _typDilaNazev;
        private string _technikaNazev;
        private string _rozmer;
        private bool _skladem;

        //private string _majitel1Jmeno;
        //private string _majitel1Prijmeni;
        //private string _majitel2Jmeno;
        //private string _majitel2Prijmeni;

        private string _umisteniNazev;

        #endregion


        [DbColumn("DiloKoupeno")]
        public DateTime Koupeno
        {
            get { return _koupeno; }
            set
            {
                if (_koupeno != value)
                {
                    _koupeno = value;
                    OnPropertyChanged("Koupeno");
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

        [DbColumn("KupniMenaNazev")]
        public string NakupMenaNazev
        {
            get { return _nakupMenaNazev; }
            set
            {
                if (_nakupMenaNazev != value)
                {
                    _nakupMenaNazev = value;
                    OnPropertyChanged("NakupMenaNazev");
                }
            }
        }

        [DbColumn("KoupenoKdeNazev")]
        public string KoupenoKdeNazev
        {
            get { return _koupenoKdeNazev; }
            set
            {
                if (_koupenoKdeNazev != value)
                {
                    _koupenoKdeNazev = value;
                    OnPropertyChanged("KoupenoKdeNazev");
                }
            }
        }


        [DbColumn("Prodano")]
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

        [DbColumn("DiloProdano")]
        public DateTime ProdanoKdy
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

        [DbColumn("ProdejMenaNazev")]
        public string ProdejMenaNazev
        {
            get { return _prodejMenaNazev; }
            set
            {
                if (_prodejMenaNazev != value)
                {
                    _prodejMenaNazev = value;
                    OnPropertyChanged("ProdejMenaNazev");
                }
            }
        }

        [DbColumn("ProdanoKdeNazev")]
        public string ProdanoKdeNazev
        {
            get { return _prodanoKdeNazev; }
            set
            {
                if (_prodanoKdeNazev != value)
                {
                    _prodanoKdeNazev = value;
                    OnPropertyChanged("ProdanoKdeNazev");
                }
            }
        }


        [DbColumn("AutorJmeno")]
        public string AutorJmeno
        {
            get { return _autorJmeno; }
            set
            {
                if (_autorJmeno != value)
                {
                    _autorJmeno = value;
                    OnPropertyChanged("AutorJmeno");
                }
            }
        }

        [DbColumn("AutorPrijmeni")]
        public string AutorPrijmeni
        {
            get { return _autorPrijmeni; }
            set
            {
                if (_autorPrijmeni != value)
                {
                    _autorPrijmeni = value;
                    OnPropertyChanged("AutorPrijmeni");
                }
            }
        }


        [DbColumn("DiloNazev", Int32.MaxValue)]
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

        [DbColumn("DiloRok")]
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

        [DbColumn("TypDilaNazev")]
        public string TypDilaNazev
        {
            get { return _typDilaNazev; }
            set
            {
                if (_typDilaNazev != value)
                {
                    _typDilaNazev = value;
                    OnPropertyChanged("TypDilaNazev");
                }
            }
        }

        [DbColumn("TechnikaNazev")]
        public string TechnikaNazev
        {
            get { return _technikaNazev; }
            set
            {
                if (_technikaNazev != value)
                {
                    _technikaNazev = value;
                    OnPropertyChanged("TechnikaNazev");
                }
            }
        }


        [DbColumn("DiloRozmer", Int32.MaxValue)]
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

        [DbColumn("DiloSkladem")]
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


        //[DbColumn("Majitel1Jmeno")]
        //public string Majitel1Jmeno
        //{
        //    get { return _majitel1Jmeno; }
        //    set
        //    {
        //        if (_majitel1Jmeno != value)
        //        {
        //            _majitel1Jmeno = value;
        //            OnPropertyChanged("Majitel1Jmeno");
        //        }
        //    }
        //}

        //[DbColumn("Majitel1Prijmeni")]
        //public string Majitel1Prijmeni
        //{
        //    get { return _majitel1Prijmeni; }
        //    set
        //    {
        //        if (_majitel1Prijmeni != value)
        //        {
        //            _majitel1Prijmeni = value;
        //            OnPropertyChanged("Majitel1Prijmeni");
        //        }
        //    }
        //}

        //[DbColumn("Majitel2Jmeno")]
        //public string Majitel2Jmeno
        //{
        //    get { return _majitel2Jmeno; }
        //    set
        //    {
        //        if (_majitel2Jmeno != value)
        //        {
        //            _majitel2Jmeno = value;
        //            OnPropertyChanged("Majitel2Jmeno");
        //        }
        //    }
        //}

        //[DbColumn("Majitel2Prijmeni")]
        //public string Majitel2Prijmeni
        //{
        //    get { return _majitel2Prijmeni; }
        //    set
        //    {
        //        if (_majitel2Prijmeni != value)
        //        {
        //            _majitel2Prijmeni = value;
        //            OnPropertyChanged("Majitel2Prijmeni");
        //        }
        //    }
        //}


        [DbColumn("UmisteniNazev")]
        public string UmisteniNazev
        {
            get { return _umisteniNazev; }
            set
            {
                if (_umisteniNazev != value)
                {
                    _umisteniNazev = value;
                    OnPropertyChanged("UmisteniNazev");
                }
            }
        }
          

        public bool NeedsUpdate(DiloDataGridItem source)
        {
            if (Id != source.Id) return true;

            if (Koupeno != source.Koupeno) return true;
            if (NakupCena != source.NakupCena) return true;
            if (NakupMenaNazev != source.NakupMenaNazev) return true;
            if (KoupenoKdeNazev != source.KoupenoKdeNazev) return true;

            if (Prodano != source.Prodano) return true;
            if (ProdanoKdy != source.ProdanoKdy) return true;
            if (ProdejCena != source.ProdejCena) return true;
            if (ProdejMenaNazev != source.ProdejMenaNazev) return true;
            if (ProdanoKdeNazev != source.ProdanoKdeNazev) return true;

            if (AutorJmeno != source.AutorJmeno) return true;
            if (AutorPrijmeni != source.AutorPrijmeni) return true;
                                      
            if (Nazev != source.Nazev) return true;
            if (Rok != source.Rok) return true;
            if (TypDilaNazev != source.TypDilaNazev) return true;
            if (TechnikaNazev != source.TechnikaNazev) return true;
            if (Rozmer != source.Rozmer) return true;
            if (Skladem != source.Skladem) return true;

            //if (Majitel1Jmeno != source.Majitel1Jmeno) return true;
            //if (Majitel1Prijmeni != source.Majitel1Prijmeni) return true;
            //if (Majitel2Jmeno != source.Majitel2Jmeno) return true;
            //if (Majitel2Prijmeni != source.Majitel2Prijmeni) return true;
            
            if (UmisteniNazev != source.UmisteniNazev) return true;

            return false;
        }


        public void Update(DiloDataGridItem source)
        {
            throw new NotImplementedException();
        }
    }
}
