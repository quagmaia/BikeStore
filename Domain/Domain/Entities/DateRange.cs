using System;

namespace Domain.Entities
{
    public class DateRange
    {
        public DateTimeOffset StartsOn { get; set; }
        public DateTimeOffset EndsOn { get; set; }

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

        public bool Equals(DateRange other)
        {
            return StartsOn.Equals(other.StartsOn) && EndsOn.Equals(other.EndsOn);
        }
    }
}
