/* (C) 2016 Premysl Fara */

namespace ArtMan.Datalayer
{
    using System;

    using ArtMan.Core.Data;
    using ArtMan.DataObjects;


    public class AutorDataLayer : LookupDataLayer<Autor>
    {
        public AutorDataLayer(Database database) 
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


        public override int GetIdByName(string name, bool bypassCache = false)
        {
            throw new NotImplementedException();
        }
    }
}
