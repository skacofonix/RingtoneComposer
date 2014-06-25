using System;

namespace RingtoneComposer.Core.Helpers
{
    public static class DurationHelper
    {
        public static Durations Increase(this Durations d)
        {
             return (Durations)Math.Max((int)Durations.Whole, (int)d / 2);
        }

        public static Durations Decrease(this Durations d)
        {
            return (Durations)Math.Min((int)Durations.ThirtySecond, (int)d * 2);
        }
    }
}
