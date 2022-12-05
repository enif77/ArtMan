/* (C) 2016 Premysl Fara */

namespace ArtMan.Datalayer
{
    using System;

    using ArtMan.Core.Data;
    using ArtMan.Core.Injektor;
                        

    public static class Initializer
    {
        public static void InitializeLayers(Database database)
        {
            if (database == null) throw new ArgumentNullException("database");

            Registry.RegisterInstance(new AutorDataLayer(database));
            Registry.RegisterInstance(new DiloDataLayer(database));
            Registry.RegisterInstance(new KurzovniListekDataLayer(database));
            Registry.RegisterInstance(new MajitelDataLayer(database));
            Registry.RegisterInstance(new MenaDataLayer(database));
            Registry.RegisterInstance(new OceneniDataLayer(database));
            Registry.RegisterInstance(new ProdejniMistoDataLayer(database));
            Registry.RegisterInstance(new TechnikaDataLayer(database));
            Registry.RegisterInstance(new TypDilaDataLayer(database));
            Registry.RegisterInstance(new UmisteniDataLayer(database));
        }
    }
}
