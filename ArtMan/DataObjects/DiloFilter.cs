/* (C) 2016 Premysl Fara */

namespace ArtMan.DataObjects
{
    using System;

    using ArtMan.Core.Data;


    public class DiloFilter : ADataObject
    {
        #region fields

        private DateTime _koupenoOd;
        private DateTime _koupenoOdUi;
        private bool _useKoupenoOd;

        private DateTime _koupenoDo;
        private DateTime _koupenoDoUi;
        private bool _useKoupenoDo;

        private DateTime _prodanoOd;
        private DateTime _prodanoOdUi;
        private bool _useProdanoOd;

        private DateTime _prodanoDo;
        private DateTime _prodanoDoUi;
        private bool _useProdanoDo;

        private int _skladem;
        private int _prodano;

        private int _autorId;

        #endregion


        #region properties

        public DateTime KoupenoOd
        {
            get { return _koupenoOd; }
            set
            {
                if (value != _koupenoOd)
                {
                    _koupenoOd = value;
                    OnPropertyChanged("KoupenoOd");
                }
            }
        }

        public DateTime KoupenoOdUi
        {
            get { return _koupenoOdUi; }
            set
            {
                if (value != _koupenoOdUi)
                {
                    _koupenoOdUi = value;
                    OnPropertyChanged("KoupenoOdUi");
                }
            }
        }

        public bool UseKoupenoOd
        {
            get { return _useKoupenoOd; }
            set
            {
                if (value != _useKoupenoOd)
                {
                    _useKoupenoOd = value;
                    OnPropertyChanged("UseKoupenoOd");
                }
            }
        }


        public DateTime KoupenoDo
        {
            get { return _koupenoDo; }
            set
            {
                if (value != _koupenoDo)
                {
                    _koupenoDo = value;
                    OnPropertyChanged("KoupenoDo");
                }
            }
        }

        public DateTime KoupenoDoUi
        {
            get { return _koupenoDoUi; }
            set
            {
                if (value != _koupenoDoUi)
                {
                    _koupenoDoUi = value;
                    OnPropertyChanged("KoupenoDoUi");
                }
            }
        }

        public bool UseKoupenoDo
        {
            get { return _useKoupenoDo; }
            set
            {
                if (value != _useKoupenoDo)
                {
                    _useKoupenoDo = value;
                    OnPropertyChanged("UseKoupenoDo");
                }
            }
        }


        public DateTime ProdanoOd
        {
            get { return _prodanoOd; }
            set
            {
                if (value != _prodanoOd)
                {
                    _prodanoOd = value;
                    OnPropertyChanged("ProdanoOd");
                }
            }
        }

        public DateTime ProdanoOdUi
        {
            get { return _prodanoOdUi; }
            set
            {
                if (value != _prodanoOdUi)
                {
                    _prodanoOdUi = value;
                    OnPropertyChanged("ProdanoOdUi");
                }
            }
        }

        public bool UseProdanoOd
        {
            get { return _useProdanoOd; }
            set
            {
                if (value != _useProdanoOd)
                {
                    _useProdanoOd = value;
                    OnPropertyChanged("UseProdanoOd");
                }
            }
        }


        public DateTime ProdanoDo
        {
            get { return _prodanoDo; }
            set
            {
                if (value != _prodanoDo)
                {
                    _prodanoDo = value;
                    OnPropertyChanged("ProdanoDo");
                }
            }
        }

        public DateTime ProdanoDoUi
        {
            get { return _prodanoDoUi; }
            set
            {
                if (value != _prodanoDoUi)
                {
                    _prodanoDoUi = value;
                    OnPropertyChanged("ProdanoDoUi");
                }
            }
        }

        public bool UseProdanoDo
        {
            get { return _useProdanoDo; }
            set
            {
                if (value != _useProdanoDo)
                {
                    _useProdanoDo = value;
                    OnPropertyChanged("UseProdanoDo");
                }
            }
        }


        public int Skladem
        {
            get { return _skladem; }
            set
            {
                if (value != _skladem)
                {
                    _skladem = value;
                    OnPropertyChanged("Skladem");
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

        #endregion


        #region ctor

        public DiloFilter()
        {
            KoupenoOd = DateTime.Today.AddDays(-30);
            KoupenoOdUi = KoupenoOd;
            UseKoupenoOd = false;

            KoupenoDo = DateTime.Today;
            KoupenoDoUi = KoupenoDo;
            UseKoupenoDo = false;

            ProdanoOd = DateTime.Today;
            ProdanoOdUi = ProdanoOd;
            UseProdanoOd = false;

            ProdanoDo = DateTime.Today;
            ProdanoDoUi = ProdanoDo;
            UseProdanoDo = false;

            Prodano = -1;
            Skladem = -1;
        }

        #endregion


        #region public methods

        public void Clear()
        {
            Prodano = -1;
            Skladem = -1;
        }

        public void ClearAllExceptDates()
        {
            Clear();
            ClearSingleSelects();
        }

        #endregion


        #region non-public methods

        protected virtual void ClearSingleSelects()
        {
            AutorId = 0;
        }

        #endregion
    }
}
