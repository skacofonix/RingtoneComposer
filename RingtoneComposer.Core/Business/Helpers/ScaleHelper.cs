using System;

namespace RingtoneComposer.Core.Helpers
{
    public static class ScaleHelper
    {
        public static Scales Increase(this Scales s)
        {
            return (Scales)Math.Min((int)Scales.Seven, (int)s + 1);
        }

        public static Scales IncreaseModulo(this Scales s)
        {
            if (s == Scales.Seven)
                return Scales.Four;
            return Increase(s);
        }

        public static Scales Decrease(this Scales s)
        {
            return (Scales)Math.Max((int)Scales.Four, (int)s - 1);
        }
    }
}
