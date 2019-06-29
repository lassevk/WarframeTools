using System;

using WarframeTools.Relics.Models;

namespace WarframeTools.Relics
{
    public class InventoriedRelic
    {
        public InventoriedRelic(RefinedRelic relic, int amount)
        {
            Relic = relic;
            Amount = amount;
        }

        public RefinedRelic Relic { get; }

        public int Amount { get; }

        public override string ToString()
        {
            if (Amount == 0)
                return $"No {Relic}";

            return $"{Amount}x {Relic}";
        }
    }
}