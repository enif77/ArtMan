/* (C) 2016 premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class ProdejniMistoDataLayer : LookupDataLayer<ProdejniMisto>
    {
        public ProdejniMistoDataLayer(Database database)
            : base(database)
        {
        }
    }
}
