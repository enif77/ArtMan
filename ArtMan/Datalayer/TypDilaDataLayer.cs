/* (C) 2016 Premysl Fara */

namespace ArtMan.Datalayer
{
    using ArtMan.Core.Data;
    using ArtMan.DataObjects;


    public class TypDilaDataLayer : LookupDataLayer<TypDila>
    {
        public TypDilaDataLayer(Database database)
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
