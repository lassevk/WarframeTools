using System.Collections.Generic;

using JetBrains.Annotations;

namespace WarframeTools.Relics.Models
{
    public class InventoriedRelicModel
    {
        public string Id { get; set; }

        [NotNull]
        public Dictionary<int, int> Amount { get; } = new Dictionary<int, int>();
    }
}