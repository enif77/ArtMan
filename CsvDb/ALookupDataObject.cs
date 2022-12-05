/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    using System; 


    /// <summary>
    /// A base class for a business object.
    /// </summary>
    public abstract class ALookupDataObject<T> : AIdDataObject, ILookup, ICloneable, IUpdatable<T> where T : class, ILookup, new()
    {
        #region public fields

        public static readonly T RequiredValue = EmptyValue<T>.Required;
        public static readonly T OptionalValue = EmptyValue<T>.Optional;

        #endregion


        #region fields

        private string _name;
        private string _description;

        #endregion


        #region properties

        [DbColumn("Name", Int32.MaxValue)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }


        [DbColumn("Description", Int32.MaxValue)]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        #endregion


        #region public methods

        public override string ToString()
        {
            return Name;
        }


        public virtual object Clone()
        {
            return new T()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description

            };
        }

        #endregion


        #region IUpdatable<T>

        public virtual bool NeedsUpdate(T source)
        {
            if (Id != source.Id) return true;
            if (Name != source.Name) return true;
            if (Description != source.Description) return true;

            return false;
        }


        public virtual void Update(T source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
        }

        #endregion
    }
}
