/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class TypDilaDataLayer : LookupDataLayer<TypDila>
    {
        public TypDilaDataLayer(Database database)
            : base(database)
        {
        }
    }
}
