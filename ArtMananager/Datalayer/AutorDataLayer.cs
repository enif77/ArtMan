/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using System;

    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class AutorDataLayer : LookupDataLayer<Autor>
    {
        public AutorDataLayer(Database database) 
            : base(database)
        {
        }


        public override int GetIdByName(string name, bool bypassCache = false)
        {
            throw new NotImplementedException();
        }
    }
}
