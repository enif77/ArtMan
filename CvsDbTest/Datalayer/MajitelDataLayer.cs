/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using System;

    using CsvDb;
    using CvsDbTest.DataObjects;


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
