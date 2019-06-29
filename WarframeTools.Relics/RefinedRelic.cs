using System;
using System.Collections.Generic;
using System.Linq;

namespace WarframeTools.Relics
{
    public struct RefinedRelic
    {
        public RefinedRelic(Relic relic, Refinement refinement)
        {
            Relic = relic;
            Refinement = refinement;
        }

        public Relic Relic { get; }

        public Refinement Refinement { get; }

        public override string ToString() => $"{Refinement} {Relic}";

        public IEnumerable<AnnotatedReward> GetRewards()
        {
            var rewards = Relic.Rewards.ToList();

            double commonChance;
            double uncommonChance;
            double rareChance;
            
            switch (Refinement)
            {
                case Refinement.Intact:
                    commonChance = 76;
                    uncommonChance = 22;
                    rareChance = 2;
                    break;

                case Refinement.Exceptional:
                    commonChance = 70;
                    uncommonChance = 26;
                    rareChance = 4;
                    break;

                case Refinement.Flawless:
                    commonChance = 60;
                    uncommonChance = 34;
                    rareChance = 6;
                    break;

                case Refinement.Radiant:
                    commonChance = 50;
                    uncommonChance = 40;
                    rareChance = 10;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            yield return new AnnotatedReward(Rarity.Common, rewards[0], commonChance / 3);
            yield return new AnnotatedReward(Rarity.Common, rewards[1], commonChance / 3);
            yield return new AnnotatedReward(Rarity.Common, rewards[2], commonChance / 3);
            yield return new AnnotatedReward(Rarity.Uncommon, rewards[3], uncommonChance / 2);
            yield return new AnnotatedReward(Rarity.Uncommon, rewards[4], uncommonChance / 2);
            yield return new AnnotatedReward(Rarity.Rare, rewards[5], rareChance);
        }
    }
}