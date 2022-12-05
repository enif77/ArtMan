/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class MenaDataLayer : LookupDataLayer<Mena>
    {
        public MenaDataLayer(Database database)
            : base(database)
        {
        }
    }
}
