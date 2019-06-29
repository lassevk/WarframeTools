using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using JetBrains.Annotations;

using LVK.Json;
using LVK.Resources;

using Newtonsoft.Json;

namespace WarframeTools.Relics
{
    internal class RelicTable : IRelicTable
    {
        [NotNull]
        private readonly List<Relic> _AllRelics = new List<Relic>();

        [NotNull]
        private readonly IResources _Resources;

        [NotNull]
        private readonly Dictionary<RelicId, Relic> _Lookup = new Dictionary<RelicId, Relic>();

        public RelicTable([NotNull] IResourcesFactory resourcesFactory)
        {
            _Resources = resourcesFactory.GetResources<RelicTable>();
        }

        public IEnumerable<Relic> GetAllRelics()
        {
            LoadRelicsIfNeeded();
            return _AllRelics;
        }

        public Relic LookupById(RelicId id)
        {
            LoadRelicsIfNeeded();
            return _Lookup[id];
        }

        private void LoadRelicsIfNeeded()
        {
            if (_AllRelics.Count > 0)
                return;

            _AllRelics.AddRange(_Resources.DeserializeJson<List<Relic>>("Resources.Relics.Lith.json"));
            _AllRelics.AddRange(_Resources.DeserializeJson<List<Relic>>("Resources.Relics.Meso.json"));
            _AllRelics.AddRange(_Resources.DeserializeJson<List<Relic>>("Resources.Relics.Neo.json"));
            _AllRelics.AddRange(_Resources.DeserializeJson<List<Relic>>("Resources.Relics.Axi.json"));

            foreach (var relic in _AllRelics)
                _Lookup.Add(relic.Id, relic);
        }
    }
}