/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class UmisteniDataLayer : LookupDataLayer<Umisteni>
    {
        public UmisteniDataLayer(Database database)
            : base(database)
        {
        }
    }
}
