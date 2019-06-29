using DryIoc;

using LVK.AppCore;
using LVK.DryIoc;

namespace WarframeTools
{
    internal class ServicesBootstrapper : IServicesBootstrapper
    {
        public void Bootstrap(IContainer container)
        {
            container.Bootstrap<Relics.Commands.ServicesBootstrapper>();
            container.Bootstrap<Relics.ServicesBootstrapper>();
        }
    }
}