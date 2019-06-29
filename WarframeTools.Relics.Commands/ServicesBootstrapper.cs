using DryIoc;

using LVK.AppCore;
using LVK.DryIoc;

namespace WarframeTools.Relics.Commands
{
    public class ServicesBootstrapper : IServicesBootstrapper
    {
        public void Bootstrap(IContainer container)
        {
            container.Bootstrap<LVK.Commands.ServicesBootstrapper>();

            container.Register<IApplicationCommand, RelicStatisticsCommand>();
            container.Register<IApplicationCommand, RelicLookupCommand>();
            container.Register<IApplicationCommand, DucatFarmCommand>();
        }
    }
}