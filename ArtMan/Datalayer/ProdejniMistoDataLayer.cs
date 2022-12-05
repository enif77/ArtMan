/* (C) 2016 premysl Fara */

namespace ArtMan.Datalayer
{
    using ArtMan.Core.Data;
    using ArtMan.DataObjects;


    public class ProdejniMistoDataLayer : LookupDataLayer<ProdejniMisto>
    {
        public ProdejniMistoDataLayer(Database database)
            : base(database)
        {
        }
                    

        /// <summary>
        /// The security is not needed for this data layer.
        /// </summary>
        protected override bool BypassSecurity
        {
            get
            {
                return true;
            }
        }
    }
}
