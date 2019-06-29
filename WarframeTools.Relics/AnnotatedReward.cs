namespace WarframeTools.Relics
{
    public struct AnnotatedReward
    {
        internal AnnotatedReward(Rarity rarity, string name, double dropChance)
        {
            DropChance = dropChance;
            Rarity = rarity;
            Name = name;
        }

        public double DropChance { get; }

        public Rarity Rarity { get; }

        public string Name { get; }
    }
}