/* (C) 2016 Premysl Fara */

namespace CvsDbTest.DataObjects
{
    using System;

    using CsvDb;
    using CvsDbTest.Core;


    [DbTable("Technika")]
    public class Technika : AIdDataObject, ILookup, IUpdatable<Technika>
    {
        #region public fields

        public static readonly Technika RequiredValue = EmptyValue<Technika>.Required;
        public static readonly Technika OptionalValue = EmptyValue<Technika>.Optional;

        #endregion


        #region fields

        private int _id;
        private string _nazev;
        private string _popis;

        #endregion


        #region ctor

        public Technika()
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


        public bool NeedsUpdate(Technika source)
        {
            if (Id != source.Id) return true;
            if (Nazev != source.Nazev) return true;
            if (Popis != source.Popis) return true;

            return false;
        }


        public void Update(Technika source)
        {
            throw new NotImplementedException();
        }
    }
}
