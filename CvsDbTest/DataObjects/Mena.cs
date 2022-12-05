/* (C) 2016 Premysl Fara */

namespace CvsDbTest.DataObjects
{
    using System;

    using CsvDb;
    using CvsDbTest.Core;


    [DbTable("Mena")]
    public class Mena : AIdDataObject, ILookup, IUpdatable<Mena>
    {
        #region public fields

        public static readonly Mena RequiredValue = EmptyValue<Mena>.Required;
        public static readonly Mena OptionalValue = EmptyValue<Mena>.Optional;

        #endregion


        #region fields

        private int _id;
        private string _nazev;
        private string _popis;

        #endregion


        #region ctor
                   
        public Mena()
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
          
        [DbColumn("Nazev", 3)]
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


        public bool NeedsUpdate(Mena source)
        {
            if (Id != source.Id) return true;
            if (Nazev != source.Nazev) return true;
            if (Popis != source.Popis) return true;

            return false;
        }

                       
        public void Update(Mena source)
        {
            throw new NotImplementedException();
        }
    }
}
