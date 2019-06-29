using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JetBrains.Annotations;

using LVK.AppCore;

namespace WarframeTools.Relics.Commands
{
    public class DucatFarmCommand : IApplicationCommand
    {
        [NotNull]
        private readonly IRelicTable _RelicTable;

        [NotNull]
        private readonly IRelicInventory _RelicInventory;

        [NotNull]
        private readonly IItemDatabase _ItemDatabase;

        public DucatFarmCommand([NotNull] IRelicTable relicTable, [NotNull] IRelicInventory relicInventory, [NotNull] IItemDatabase itemDatabase)
        {
            _RelicTable = relicTable ?? throw new ArgumentNullException(nameof(relicTable));
            _RelicInventory = relicInventory ?? throw new ArgumentNullException(nameof(relicInventory));
            _ItemDatabase = itemDatabase ?? throw new ArgumentNullException(nameof(itemDatabase));
        }

        public string[] CommandNames => new[] { "ducat-farm" };

        public string Description => "Shows best relic to farm for ducats";

        public Task<int> TryExecute(string[] arguments)
        {
            List<InventoriedRelic> relics;
            if (arguments.Length == 1)
            {
                var era = (Era)Enum.Parse(typeof(Era), arguments[0]);
                relics = _RelicInventory.GetInventory().Where(r => r.Relic.Relic.Id.Era == era).ToList();
            }
            else
                relics = _RelicInventory.GetInventory().ToList();

            var relicWorths = new List<(InventoriedRelic relic, double worth)>();
            foreach (var relic in relics)
            {
                var worth = 0.0;
                foreach (var reward in relic.Relic.GetRewards())
                {
                    var item = _ItemDatabase.LookupByName(reward.Name);
                    worth += item.Ducats * reward.DropChance / 100.0;
                }

                relicWorths.Add((relic, worth));
            }

            foreach (var relicWorth in relicWorths.OrderByDescending(rw => rw.worth))
                Console.WriteLine($"{relicWorth.worth,5:0.00} {relicWorth.relic}");

            return Task.FromResult(0);
        }
    }
}