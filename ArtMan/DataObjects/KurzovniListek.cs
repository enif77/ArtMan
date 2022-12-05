/* (C) 2016 Premysl Fara */

namespace ArtMan.DataObjects
{
    using System;

    using ArtMan.Core.Data;


    [DbTable("KurzovniListek")]
    public class KurzovniListek : AIdDataObject
    {
        #region fields

        private int _menaId;
        private decimal _eurRate;
        private DateTime _datum;

        #endregion


        #region ctor

        public KurzovniListek()
        {
        }

        #endregion


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

        [DbColumn("EurRate")]
        public decimal EurRate
        {
            get { return _eurRate; }
            set
            {
                if (_eurRate != value)
                {
                    _eurRate = value;
                    OnPropertyChanged("EurRate");
                }
            }
        }

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
    }
}
