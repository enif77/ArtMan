/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class OceneniDataLayer : ABaseDatalayer<Oceneni>
    {
        public OceneniDataLayer(Database database) 
            : base(database)
        {
        }
    }
}
