/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class UmisteniDataLayer : LookupDataLayer<Umisteni>
    {
        public UmisteniDataLayer(Database database)
            : base(database)
        {
        }
    }
}
