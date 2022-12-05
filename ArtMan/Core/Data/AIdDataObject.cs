/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Data
{
    using System.Globalization;


    /// <summary>
    /// A base class for a business object.
    /// </summary>
    public abstract class AIdDataObject : ADataObject, IId
    {
        #region fields

        private int _id;

        #endregion


        #region properties

        [DbColumn("Id", 1, DbColumnAttribute.ColumnOptions.Id)]
        public virtual int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        #endregion


        #region public methods

        public override string ToString()
        {
            return Id.ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}
