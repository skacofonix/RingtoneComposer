using System;

namespace RingtoneComposer.Core.Converter
{
    public class ScaleConverter : IConverter<Scales>
    {
        public string ToString(Scales noteEnum)
        {
            return ((int) noteEnum).ToString();
        }

        public Scales Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("s");

            var stringNormalized = s.Trim();
            if(stringNormalized.Length != 1)
                throw new ArgumentOutOfRangeException("s");

            if(stringNormalized[0] < '4' || stringNormalized[0] > '7')
                throw new ArgumentOutOfRangeException("s");

            Scales scale;
            if(!Enum.TryParse(stringNormalized, out scale))
                throw new FormatException("s");

            return scale;
        }

        public int GetValue(Scales scaleEnum)
        {
            return (int) scaleEnum;
        }
    }
}
