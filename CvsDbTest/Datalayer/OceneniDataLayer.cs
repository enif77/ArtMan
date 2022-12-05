/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class OceneniDataLayer : ABaseDatalayer<Oceneni>
    {
        public OceneniDataLayer(Database database) 
            : base(database)
        {
        }
    }
}
