/* (C) 2016 Premysl Fara */
    
namespace ArtMananager.Datalayer
{
    using System;

    using SimpleDb.Files;
    using Injektor;


    public static class Initializer
    {
        public static void InitializeLayers(Database database)
        {
            if (database == null) throw new ArgumentNullException("database");

            Registry.RegisterInstance(new ConfigurationDataLayer(database));

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

            // Initialize DALs, load current data to memory.
            Registry.Get<ConfigurationDataLayer>().GetAll();

            Registry.Get<MenaDataLayer>().GetAll();
            Registry.Get<OceneniDataLayer>().GetAll();
            Registry.Get<ProdejniMistoDataLayer>().GetAll();
            Registry.Get<TechnikaDataLayer>().GetAll();
            Registry.Get<TypDilaDataLayer>().GetAll();
            Registry.Get<UmisteniDataLayer>().GetAll();
            Registry.Get<KurzovniListekDataLayer>().GetAll();
            Registry.Get<MajitelDataLayer>().GetAll();
            Registry.Get<AutorDataLayer>().GetAll();
            Registry.Get<DiloDataLayer>().GetAll();
        }
    }
}
