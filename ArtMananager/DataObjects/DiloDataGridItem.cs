/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("Dilo")]
    public class DiloDataGridItem : AIdDataObject, IUpdatable<DiloDataGridItem>
    {
        #region fields

        private int _koupenoRok;
        private decimal _nakupCena;
        private decimal _nakupCenaCzk;
        private decimal _dnesniCenaCzk;
        private string _nakupMenaNazev;
        private string _koupenoKdeNazev;

        private bool _pojisteno;

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

        private string _majitel;

        private string _umisteniNazev;

        private int _kusu;

        #endregion


        [DbColumn("DiloKoupenoRok")]
        public int KoupenoRok
        {
            get { return _koupenoRok; }
            set
            {
                if (_koupenoRok != value)
                {
                    _koupenoRok = value;
                    OnPropertyChanged("KoupenoRok");
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
                       
        [DbColumn("Majitel")]
        public string Majitel
        {
            get { return _majitel; }
            set
            {
                if (_majitel != value)
                {
                    _majitel = value;
                    OnPropertyChanged("Majitel");
                }
            }
        }

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


        public bool NeedsUpdate(DiloDataGridItem source)
        {
            if (Id != source.Id) return true;

            if (KoupenoRok != source.KoupenoRok) return true;
            if (NakupCena != source.NakupCena) return true;
            if (NakupCenaCzk != source.NakupCenaCzk) return true;
            if (DnesniCenaCzk != source.DnesniCenaCzk) return true;
            if (NakupMenaNazev != source.NakupMenaNazev) return true;
            if (KoupenoKdeNazev != source.KoupenoKdeNazev) return true;
            if (Pojisteno != source.Pojisteno) return true;
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
            if (Majitel != source.Majitel) return true;
            if (UmisteniNazev != source.UmisteniNazev) return true;
            if (Kusu != source.Kusu) return true;

            return false;
        }


        public void Update(DiloDataGridItem source)
        {
            throw new NotImplementedException();
        }
    }
}
