using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using JetBrains.Annotations;

using LVK.Configuration;
using LVK.Core;
using LVK.Json;

using Newtonsoft.Json;

using WarframeTools.Relics.Models;

namespace WarframeTools.Relics
{
    public interface IRelicInventory
    {
        IEnumerable<InventoriedRelic> GetInventory();
    }

    public class RelicInventory : IRelicInventory
    {
        [NotNull]
        private readonly IJsonSerializerFactory _JsonSerializerFactory;

        [NotNull]
        private readonly IRelicTable _RelicTable;

        [NotNull]
        private readonly IConfigurationElementWithDefault<string> _UserdataConfiguration;

        public RelicInventory([NotNull] IConfiguration configuration, [NotNull] IJsonSerializerFactory jsonSerializerFactory, [NotNull] IRelicTable relicTable)
        {
            _JsonSerializerFactory = jsonSerializerFactory ?? throw new ArgumentNullException(nameof(jsonSerializerFactory));
            _RelicTable = relicTable ?? throw new ArgumentNullException(nameof(relicTable));
            _UserdataConfiguration = configuration.Element<string>("Userdata").WithDefault(() => "");
        }

        public IEnumerable<InventoriedRelic> GetInventory()
        {
            string inventoryFilePath = GetInventoryFilePath();
            if (!File.Exists(inventoryFilePath))
                return Enumerable.Empty<InventoriedRelic>();

            var models = _JsonSerializerFactory.Create()
               .DeserializeObjectFromFile<List<InventoriedRelicModel>>(inventoryFilePath).NotNull()
               .ToList();

            var result = new List<InventoriedRelic>();
            foreach (var model in models)
            {
                var relic = _RelicTable.LookupById(RelicId.Parse(model.Id));
                
                for (int index = 0; index < 4; index++)
                    if (model.Amount.ContainsKey(index) && model.Amount[index] > 0)
                    {
                        var refinedRelic = new RefinedRelic(relic, (Refinement)index);
                        var inventoriedRelic = new InventoriedRelic(refinedRelic, model.Amount[index]);
                        result.Add(inventoriedRelic);
                    }
            }

            return result;
        }

        [NotNull]
        private string GetInventoryFilePath() => Path.Combine(_UserdataConfiguration.Value(), "RelicInventory.json");
    }
}