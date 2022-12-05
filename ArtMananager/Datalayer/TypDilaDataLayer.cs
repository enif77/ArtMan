/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class TypDilaDataLayer : LookupDataLayer<TypDila>
    {
        public TypDilaDataLayer(Database database)
            : base(database)
        {
        }
    }
}
