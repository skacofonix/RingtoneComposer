using System;

namespace RingtoneComposer.Core.Converter
{
    public class PitchConverter : IConverter<Pitchs>
    {
        public string ToString(Pitchs noteEnum)
        {
            switch (noteEnum)
            {
                case Pitchs.A:
                case Pitchs.B:
                case Pitchs.C:
                case Pitchs.D:
                case Pitchs.E:
                case Pitchs.F:
                case Pitchs.G:
                    return noteEnum.ToString();
                case Pitchs.Asharp:
                    return "A#";
                case Pitchs.Csharp:
                    return "C#";
                case Pitchs.Dsharp:
                    return "D#";
                case Pitchs.Fsharp:
                    return "F#";
                case Pitchs.Gsharp:
                    return "G#";
                default :
                    throw new ArgumentOutOfRangeException("noteEnum");
            }
        }

        public Pitchs Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException();

            var stringNormalized = s.Trim().ToUpperInvariant();
            if (stringNormalized.Contains("#"))
                stringNormalized = String.Concat(stringNormalized.Replace("#", ""), "sharp");

            Pitchs result;
            if (!Enum.TryParse(stringNormalized, false, out result))
                throw new InvalidCastException();

            return result;
        }
    }
}