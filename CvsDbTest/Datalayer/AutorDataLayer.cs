/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using System;

    using CsvDb;
    using CvsDbTest.DataObjects;


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
