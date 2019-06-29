using System;
using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using LVK.Resources;

using WarframeTools.Relics.Models;

namespace WarframeTools.Relics
{
    public interface IItemDatabase
    {
        [NotNull]
        IEnumerable<Item> GetAll();

        Item LookupByName([NotNull] string name);
    }

    internal class ItemDatabase : IItemDatabase
    {
        [NotNull]
        private readonly List<Item> _AllItems = new List<Item>();

        [NotNull]
        private readonly Dictionary<string, Item> _Lookup = new Dictionary<string, Item>(StringComparer.InvariantCultureIgnoreCase);

        [NotNull]
        private readonly IResources _Resources;

        public ItemDatabase([NotNull] IResourcesFactory resourcesFactory)
        {
            _Resources = resourcesFactory.GetResources<ItemDatabase>();
        }

        public IEnumerable<Item> GetAll()
        {
            LoadItemsIfNeeded();
            return _AllItems;
        }

        public Item LookupByName(string name)
        {
            LoadItemsIfNeeded();
            return _Lookup[name];
        }

        private void LoadItemsIfNeeded()
        {
            if (_AllItems.Count > 0)
                return;

            var items = _Resources.DeserializeJson<Dictionary<string, ItemModel>>("Resources.Items.json");
            foreach (var kvp in items)
            {
                var item = new Item(kvp.Key, kvp.Value.Ducats);
                
                _AllItems.Add(item);
                _Lookup.Add(kvp.Key, item);
            }
        }
    }
}