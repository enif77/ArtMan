/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using CsvDb;
    using CvsDbTest.DataObjects;


    public class KurzovniListekDataLayer : ABaseDatalayer<KurzovniListek>
    {
        public KurzovniListekDataLayer(Database database) 
            : base(database)
        {
        }
    }
}
