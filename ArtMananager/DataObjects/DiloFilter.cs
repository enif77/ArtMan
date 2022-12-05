/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using SimpleDb.Shared;


    public sealed class DiloFilter : ADataObject
    {
        #region fields

        private int? _koupenoOdRok;
        private int? _koupenoDoRok;

        private int _prodano;

        private int _autorId;
        private int _umisteniId;
        private int _koupenoKdeId;
        private int _majitelId;

        // Accumulators.
        private int _rowAcc;
        private int _diloAcc;
        private decimal _nakupCenaCzkAcc;
        private decimal _dnesniCenaCzkAcc;

        #endregion


        #region properties

        public int? KoupenoOdRok
        {
            get { return _koupenoOdRok; }
            set
            {
                if (value != _koupenoOdRok)
                {
                    _koupenoOdRok = value;
                    OnPropertyChanged("KoupenoOdRok");
                }
            }
        }

        public int? KoupenoDoRok
        {
            get { return _koupenoDoRok; }
            set
            {
                if (value != _koupenoDoRok)
                {
                    _koupenoDoRok = value;
                    OnPropertyChanged("KoupenoDoRok");
                }
            }
        }

        public int Prodano
        {
            get { return _prodano; }
            set
            {
                if (value != _prodano)
                {
                    _prodano = value;
                    OnPropertyChanged("Prodano");
                }
            }
        }

        public int AutorId
        {
            get { return _autorId; }
            set
            {
                if (value != _autorId)
                {
                    _autorId = value;
                    OnPropertyChanged("AutorId");
                }
            }
        }

        public int UmisteniId
        {
            get { return _umisteniId; }
            set
            {
                if (value != _umisteniId)
                {
                    _umisteniId = value;
                    OnPropertyChanged("UmisteniId");
                }
            }
        }

        public int KoupenoKdeId
        {
            get { return _koupenoKdeId; }
            set
            {
                if (value != _koupenoKdeId)
                {
                    _koupenoKdeId = value;
                    OnPropertyChanged("KoupenoKdeId");
                }
            }
        }

        public int MajitelId
        {
            get { return _majitelId; }
            set
            {
                if (value != _majitelId)
                {
                    _majitelId = value;
                    OnPropertyChanged("MajitelId");
                }
            }
        }


        public int RowAcc
        {
            get { return _rowAcc; }
            set
            {
                if (value != _rowAcc)
                {
                    _rowAcc = value;
                    OnPropertyChanged("RowAcc");
                }
            }
        }

        public int DiloAcc
        {
            get { return _diloAcc; }
            set
            {
                if (value != _diloAcc)
                {
                    _diloAcc = value;
                    OnPropertyChanged("DiloAcc");
                }
            }
        }

        public decimal NakupCenaCzkAcc
        {
            get { return _nakupCenaCzkAcc; }
            set
            {
                if (value != _nakupCenaCzkAcc)
                {
                    _nakupCenaCzkAcc = value;
                    OnPropertyChanged("NakupCenaCzkAcc");
                }
            }
        }

        public decimal DnesniCenaCzkAcc
        {
            get { return _dnesniCenaCzkAcc; }
            set
            {
                if (value != _dnesniCenaCzkAcc)
                {
                    _dnesniCenaCzkAcc = value;
                    OnPropertyChanged("DnesniCenaCzkAcc");
                }
            }
        }

        #endregion


        #region ctor

        public DiloFilter()
        {
            KoupenoOdRok = null;
            KoupenoDoRok = null;

            Clear();
        }

        #endregion


        #region public methods

        public void Clear()
        {
            Prodano = -1;

            RowAcc = 0;
            DiloAcc = 0;
            NakupCenaCzkAcc = 0;
            DnesniCenaCzkAcc = 0;
        }

        public void ClearAllExceptDates()
        {
            Clear();
            ClearSingleSelects();
        }

        #endregion


        #region non-public methods

        private void ClearSingleSelects()
        {          
            AutorId = 0;
            UmisteniId = 0;
            KoupenoKdeId = 0;
            MajitelId = 0;
        }

        #endregion
    }
}
