/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class TechnikaDataLayer : LookupDataLayer<Technika>
    {
        public TechnikaDataLayer(Database database)
            : base(database)
        {
        }
    }
}
