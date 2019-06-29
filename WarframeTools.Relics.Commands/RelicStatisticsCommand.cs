using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JetBrains.Annotations;

using LVK.AppCore;

namespace WarframeTools.Relics.Commands
{
    internal class RelicStatisticsCommand : IApplicationCommand
    {
        [NotNull]
        private readonly IRelicInventory _Inventory;

        public RelicStatisticsCommand([NotNull] IRelicInventory inventory)
        {
            _Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public string[] CommandNames => new[] { "relic-statistics", "relic-stats" };

        public string Description => "Shows relic inventory statistics";

        public Task<int> TryExecute(string[] arguments)
        {
            var inventory = _Inventory.GetInventory().ToList();

            void showStats(string category, List<InventoriedRelic> relics)
            {
                if (relics.Count == 0)
                    Console.WriteLine($"{category, -5}: None");
                else
                    Console.WriteLine($"{category, -5}: {relics.Count, 4} unique, {relics.Sum(r => r.Amount), 4} total");
            }

            foreach (Era era in Enum.GetValues(typeof(Era)))
                showStats(era.ToString(), inventory.Where(relic => relic.Relic.Relic.Id.Era == era).ToList());

            showStats("All", inventory);

            return Task.FromResult(0);
        }
    }
}