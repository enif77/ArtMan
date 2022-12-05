/* (C) 2016 Premysl Fara */

namespace ArtMan.DataObjects
{
    using System;

    using ArtMan.Core.Data;


    [DbTable("Technika")]
    public sealed class Technika : ALookupDataObject<Technika>
    {

        #region ctor

        public Technika()
        {
            Description = String.Empty;
        }

        #endregion


        #region properties

        [DbColumn("Nazev", Int32.MaxValue)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [DbColumn("Popis", Int32.MaxValue)]
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }

        #endregion
    }
}
