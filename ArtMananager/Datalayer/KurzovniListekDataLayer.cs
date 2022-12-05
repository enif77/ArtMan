/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;
    using ArtMananager.DataObjects;


    public class KurzovniListekDataLayer : ABaseDatalayer<KurzovniListek>
    {
        public KurzovniListekDataLayer(Database database) 
            : base(database)
        {
        }
    }
}
