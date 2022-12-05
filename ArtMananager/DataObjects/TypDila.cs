/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    [DbTable("TypDila")]
    public sealed class TypDila : ALookupDataObject<TypDila>
    {
        #region ctor

        public TypDila()
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
