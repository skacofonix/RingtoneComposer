using System;

namespace RingtoneComposer.Core.Converter
{
    public class PitchConverter : IConverter<Pitches>
    {
        public string ToString(Pitches noteEnum)
        {
            switch (noteEnum)
            {
                case Pitches.A:
                case Pitches.B:
                case Pitches.C:
                case Pitches.D:
                case Pitches.E:
                case Pitches.F:
                case Pitches.G:
                    return noteEnum.ToString();
                case Pitches.Asharp:
                    return "A#";
                case Pitches.Csharp:
                    return "C#";
                case Pitches.Dsharp:
                    return "D#";
                case Pitches.Fsharp:
                    return "F#";
                case Pitches.Gsharp:
                    return "G#";
                default :
                    throw new ArgumentOutOfRangeException("noteEnum");
            }
        }

        public Pitches Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException();

            var stringNormalized = s.Trim().ToUpperInvariant();
            if (stringNormalized.Contains("#"))
                stringNormalized = String.Concat(stringNormalized.Replace("#", ""), "sharp");

            Pitches result;
            if (!Enum.TryParse(stringNormalized, false, out result))
                throw new InvalidCastException();

            return result;
        }
    }
}