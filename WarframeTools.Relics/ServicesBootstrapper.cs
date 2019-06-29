using System;

using DryIoc;

using LVK.DryIoc;

namespace WarframeTools.Relics
{
    public class ServicesBootstrapper : IServicesBootstrapper
    {
        public void Bootstrap(IContainer container)
        {
            container.Bootstrap<LVK.Json.ServicesBootstrapper>();
            container.Bootstrap<LVK.Resources.ServicesBootstrapper>();
            
            container.Register<IRelicTable, RelicTable>(Reuse.Singleton);
            container.Register<IRelicInventory, RelicInventory>();
            container.Register<IItemDatabase, ItemDatabase>();
        }
    }
}