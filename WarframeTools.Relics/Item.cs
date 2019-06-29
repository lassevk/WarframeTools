using System;

using JetBrains.Annotations;

namespace WarframeTools.Relics
{
    public struct Item
    {
        public Item([NotNull] string name, int ducats)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Ducats = ducats;
        }

        [NotNull]
        public string Name { get; }

        public int Ducats { get; }
    }
}