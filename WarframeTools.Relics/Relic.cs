using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using JetBrains.Annotations;

namespace WarframeTools.Relics
{
    public class Relic
    {
        public Relic([NotNull] string id, bool isVaulted, [NotNull] List<string> rewards)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (rewards == null)
                throw new ArgumentNullException(nameof(rewards));

            if (rewards.Count != 6)
                throw new ArgumentException("rewards must contain exactly 6 rewards", nameof(rewards));

            Id = RelicId.Parse(id);
            IsVaulted = isVaulted;
            Rewards = rewards.ToList();
        }

        public RelicId Id { get; }

        public bool IsVaulted { get; }

        public IReadOnlyCollection<string> Rewards { get; }

        public override string ToString() => Id.ToString();
    }
}