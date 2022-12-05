/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class TechnikaDataLayer : LookupDataLayer<Technika>
    {
        public TechnikaDataLayer(Database database)
            : base(database)
        {
        }
    }
}
