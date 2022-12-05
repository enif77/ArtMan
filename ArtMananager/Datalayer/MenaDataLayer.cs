/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class MenaDataLayer : LookupDataLayer<Mena>
    {
        public MenaDataLayer(Database database)
            : base(database)
        {
        }
    }
}
