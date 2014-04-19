using System;

namespace RingtoneComposer.Core.Converter
{
    public class DurationConverter : IConverter<Durations>
    {
        public string ToString(Durations durationEnum)
        {
            return ((int) durationEnum).ToString();
        }

        public Durations Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            Durations result;
            switch(s.Trim())
            {
                case "1":
                    result = Durations.Whole;
                    break;
                case "2":
                    result = Durations.Half;
                    break;
                case "4":
                    result = Durations.Quarter;
                    break;
                case "8":
                    result = Durations.Eight;
                    break;
                case "16":
                    result = Durations.Sixteenth;
                    break;
                case "32":
                    result = Durations.ThirtySecond;
                    break;
                default :
                    throw new ArgumentOutOfRangeException(s);
            }

            return result;
        }

        public double GetValue(Durations durationEnum)
        {
            return 1d/(double) durationEnum;
        }
    }
}