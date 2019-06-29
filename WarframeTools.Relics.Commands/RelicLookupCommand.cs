using System;
using System.Linq;
using System.Threading.Tasks;

using JetBrains.Annotations;

using LVK.AppCore;

namespace WarframeTools.Relics.Commands
{
    public class RelicLookupCommand : IApplicationCommand
    {
        [NotNull]
        private readonly IRelicTable _RelicTable;

        [NotNull]
        private readonly IRelicInventory _RelicInventory;

        public RelicLookupCommand([NotNull] IRelicTable relicTable, [NotNull] IRelicInventory relicInventory)
        {
            _RelicTable = relicTable ?? throw new ArgumentNullException(nameof(relicTable));
            _RelicInventory = relicInventory ?? throw new ArgumentNullException(nameof(relicInventory));
        }

        public string[] CommandNames => new[] { "relic-lookup" };

        public string Description => "Looks up a relic by its id";

        public Task<int> TryExecute(string[] arguments)
        {
            var id = RelicId.Parse(string.Join(" ", arguments.Take(2)));

            var relic = _RelicTable.LookupById(id);
            Console.WriteLine(relic);

            if (relic.IsVaulted)
                Console.WriteLine(" - is vaulted");

            var inventory = _RelicInventory.GetInventory().Where(r => r.Relic.Relic.Id == id).OrderBy(r => r.Relic.Refinement).ToList();
            if (inventory.Any())
                Console.WriteLine($" - {string.Join(", ", inventory)}");
            else
                Console.WriteLine(" - none in inventory");

            var rewards = relic.Rewards.ToList();
            Console.WriteLine(" - common rewards");
            Console.WriteLine($"    {rewards[0]}");
            Console.WriteLine($"    {rewards[1]}");
            Console.WriteLine($"    {rewards[2]}");

            Console.WriteLine(" - uncommon rewards");
            Console.WriteLine($"    {rewards[3]}");
            Console.WriteLine($"    {rewards[4]}");

            Console.WriteLine(" - rare reward");
            Console.WriteLine($"    {rewards[5]}");

            return Task.FromResult(0);
        }
    }
}