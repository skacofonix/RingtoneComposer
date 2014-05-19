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

            int integer;
            if (!int.TryParse(s, out integer))
                throw new ArgumentOutOfRangeException(s);

            if (!Enum.IsDefined(typeof(Durations), integer))
                throw new ArgumentOutOfRangeException(s);

            return (Durations)Enum.ToObject(typeof(Durations), integer);
        }

        public double GetValue(Durations durationEnum)
        {
            return 1d/(double)durationEnum;
        }
    }
}