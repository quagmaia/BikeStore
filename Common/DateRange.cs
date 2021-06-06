using System;

namespace BikeCommon
{
    [Serializable]
    public class DateRange : IEquatable<DateRange>
    {
        public readonly DateTimeOffset StartsOn;
        public readonly DateTimeOffset EndsOn;
        public const string DisplayFormatInUtc = "u";

        public DateRange(DateTimeOffset startsOn, DateTimeOffset endsOn)
        {
            StartsOn = startsOn;
            EndsOn = endsOn;
            SanityCheck();
        }

        public override string ToString() => ToString(DisplayFormatInUtc);

        public string ToString(string format) => $"{StartsOn.ToString(format)} - {EndsOn.ToString(format)}";

        public override bool Equals(object obj) => obj is DateRange other && Equals(other); 
        
        public bool Equals(DateRange other) => StartsOn.Equals(other.StartsOn) && EndsOn.Equals(other.EndsOn);
        
        public override int GetHashCode() => HashCode.Combine(StartsOn, EndsOn);

        public bool Overlaps(DateRange other)
        {
            if (Covers(other))
            {
                return true;
            }

            if (other.Covers(this))
            {
                return true;
            }

            var partiallyCoversOther = Covers(other.StartsOn) || Covers(other.EndsOn);
            if (partiallyCoversOther)
            {
                return true;
            }

            var partiallyCoveredByOther = other.Covers(StartsOn) || other.Covers(EndsOn);
            return partiallyCoveredByOther;
        }

        public bool Covers(DateTimeOffset other)
        {
            var startsBeforeOther = StartsOn <= other;
            var endsAfterOther = EndsOn >= other;
            return startsBeforeOther && endsAfterOther;
        }

        public bool Covers(DateRange other)
        {
            var coversOtherStart = Covers(other.StartsOn);

            var coversOtherEnd = Covers(other.EndsOn);

            return coversOtherStart && coversOtherEnd;
        }

        private void SanityCheck()
        {
            if (StartsOn < EndsOn)
            {
                return;
            }
            throw new Exception($"Start {StartsOn} must be before end {EndsOn}");
        }
    }
}
