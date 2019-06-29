using System;
using System.Text.RegularExpressions;

namespace WarframeTools.Relics
{
    public struct RelicId : IEquatable<RelicId>
    {
        private static readonly Regex _CodePattern = new Regex(@"^[A-Z]\d+$");

        public RelicId(Era era, string code)
        {
            if (era < Era.Lith || era > Era.Axi)
                throw new ArgumentOutOfRangeException(nameof(era));

            if (code == null)
                throw new ArgumentNullException(nameof(code));

            if (!_CodePattern.IsMatch(code))
                throw new ArgumentException("code must be an uppercase letter + a number", nameof(code));

            Era = era;
            Code = code;
        }

        public Era Era { get; }

        public string Code { get; }

        public override string ToString() => $"{Era} {Code}";

        public bool Equals(RelicId other) => Era == other.Era && string.Equals(Code, other.Code);

        public override bool Equals(object obj) => obj is RelicId other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Era * 397) ^ (Code != null ? Code.GetHashCode() : 0);
            }
        }

        public static bool operator ==(RelicId left, RelicId right) => left.Equals(right);

        public static bool operator !=(RelicId left, RelicId right) => !left.Equals(right);

        public static RelicId Parse(string id)
        {
            var re = new Regex(@"^(?<era>Lith|Meso|Neo|Axi) (?<code>[A-Z]\d+)$");
            var ma = re.Match(id);
            if (!ma.Success)
                throw new FormatException($"Unable to parse relic id '{id}'");

            return new RelicId((Era)Enum.Parse(typeof(Era), ma.Groups["era"].Value), ma.Groups["code"].Value);
        }
    }
}