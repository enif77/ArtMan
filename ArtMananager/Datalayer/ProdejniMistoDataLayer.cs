/* (C) 2016 premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class ProdejniMistoDataLayer : LookupDataLayer<ProdejniMisto>
    {
        public ProdejniMistoDataLayer(Database database)
            : base(database)
        {
        }
    }
}
