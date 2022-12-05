/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using System;

    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class MajitelDataLayer : LookupDataLayer<Majitel>
    {
        public MajitelDataLayer(Database database) 
            : base(database)
        {
        }


        public override int GetIdByName(string name, bool bypassCache = false)
        {
            throw new NotImplementedException();
        }
    }
}
