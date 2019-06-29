using System.Collections.Generic;

using JetBrains.Annotations;

namespace WarframeTools.Relics
{
    public interface IRelicTable
    {
        [NotNull]
        IEnumerable<Relic> GetAllRelics();

        Relic LookupById(RelicId id);
    }
}