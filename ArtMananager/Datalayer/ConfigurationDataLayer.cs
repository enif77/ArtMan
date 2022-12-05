/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using SimpleDb.Files;

    using ArtMananager.DataObjects;


    public class ConfigurationDataLayer : ABaseDatalayer<Configuration>
    {
        public ConfigurationDataLayer(Database database) 
            : base(database)
        {
        }
    }
}
