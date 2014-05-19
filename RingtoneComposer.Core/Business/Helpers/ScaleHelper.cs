using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return (Scales)((int)s - 4 + 1 % 4 + 4);
        }

        public static Scales Decrease(this Scales s)
        {
            return (Scales)Math.Max((int)Scales.Four, (int)s - 1);
        }
    }
}
