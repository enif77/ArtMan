/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("Oceneni")]
    public class Oceneni : AIdDataObject
    {
        #region fields

        private DateTime _datum;
        private decimal _cena;
        private int _menaId;
        private int _diloId;

        #endregion


        #region ctor

        public Oceneni()
        {             
        }

        #endregion


        [DbColumn("Datum")]
        public DateTime Datum
        {
            get { return _datum; }
            set
            {
                if (_datum != value)
                {
                    _datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        [DbColumn("Cena")]
        public decimal Cena
        {
            get { return _cena; }
            set
            {
                if (_cena != value)
                {
                    _cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }

        [DbColumn("MenaId")]
        public int MenaId
        {
            get { return _menaId; }
            set
            {
                if (_menaId != value)
                {
                    _menaId = value;
                    OnPropertyChanged("MenaId");
                }
            }
        }

        [DbColumn("DiloId")]
        public int DiloId
        {
            get { return _diloId; }
            set
            {
                if (_diloId != value)
                {
                    _diloId = value;
                    OnPropertyChanged("DiloId");
                }
            }
        }
    }
}
