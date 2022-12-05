/* (C) 2016 Premysl Fara */

namespace CvsDbTest.DataObjects
{
    using System;

    using CsvDb;
    using CvsDbTest.Core;


    [DbTable("TypDila")]
    public class TypDila : AIdDataObject, ILookup, IUpdatable<TypDila>
    {
        #region public fields

        public static readonly TypDila RequiredValue = EmptyValue<TypDila>.Required;
        public static readonly TypDila OptionalValue = EmptyValue<TypDila>.Optional;

        #endregion


        #region fields

        private int _id;
        private string _nazev;
        private string _popis;

        #endregion


        #region ctor

        public TypDila()
        {
            Popis = String.Empty;
        }

        #endregion


        public string Name
        {
            get { return Nazev; }
            set { Nazev = value; }
        }

        public string Description
        {
            get { return Popis; }
            set { Popis = value; }
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
                }
            }
        }

        [DbColumn("Popis", Int32.MaxValue)]
        public string Popis
        {
            get { return _popis; }
            set
            {
                if (_popis != value)
                {
                    _popis = value;
                    OnPropertyChanged("Popis");
                    OnPropertyChanged("Description");
                }
            }
        }


        public override string ToString()
        {
            return Name;
        }


        public bool NeedsUpdate(TypDila source)
        {
            if (Id != source.Id) return true;
            if (Nazev != source.Nazev) return true;
            if (Popis != source.Popis) return true;

            return false;
        }


        public void Update(TypDila source)
        {
            throw new NotImplementedException();
        }
    }
}
