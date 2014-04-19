using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RingtoneComposer.Core.Converter
{
    public class NokiaComposerConverter : TuneConverter
    {
        private const int OffsetScale = 3;

        public override string TuneElementPattern
        {
            get { return @"([1|2|4|8|16|32]?)([-]|#?[a|A]|[b|B]|#?[c|C]|#?[d|D]|[e|E]|#?[f|F]|#?[g|G])(\.?)([1-4]?)"; }
        }
        
        public override string TuneElementDelimiter
        {
            get { return " "; }
        }
        
        public override string Pause
        {
            get { return "-"; }
        }
       
        public override int DefaultBpm
        {
            get { return 120; }
        }

        protected override string InternalToString(Tune t)
        {
            var sb = new StringBuilder();

            foreach (var tuneElement in t.TuneElementList)
            {
                if (tuneElement.Duration != Durations.Whole)
                    sb.Append(DurationConverter.ToString(tuneElement.Duration));

                var note = tuneElement as Note;
                if (note != null)
                {
                    sb.Append(PitchConverter.ToString(note.Pitch));

                    if (note.Scale != Scales.Four)
                        sb.Append((ScaleConverter.GetValue(note.Scale) - OffsetScale).ToString(CultureInfo.InvariantCulture));
                }
                else if (tuneElement is Pause)
                {
                    sb.Append(Pause);
                }

                if (tuneElement.Dotted)
                    sb.Append(Dot);

                sb.Append(TuneElementDelimiter);
            }

            return sb.ToString();
        }

        protected override Tune InternalParse(string s)
        {
            var tuneElements = new List<TuneElement>();

            var regex = new Regex(TuneElementPattern);
            foreach (Match match in regex.Matches(s))
            {
                var duration = DurationConverter.Parse(match.Groups[1].Value);

                TuneElement tuneElement = null;
                if (match.Groups[2].Value == Pause)
                {
                    tuneElement = new Pause(duration);
                }
                else
                {
                    var pitch = PitchConverter.Parse(match.Groups[2].Value);

                    var scale = Scales.Four;
                    var scaleString = match.Groups[4].Value;
                    if (!string.IsNullOrWhiteSpace(scaleString))
                    {
                        int scaleInt;
                        if (int.TryParse(scaleString, out scaleInt))
                            scale = ScaleConverter.Parse((scaleInt + OffsetScale).ToString(CultureInfo.InvariantCulture));
                    }

                    tuneElement = new Note(pitch, scale, duration);
                }

                tuneElement.Dotted = match.Groups[3].Value == Dot;

                tuneElements.Add(tuneElement);
            }

            return new Tune(tuneElements);
        }
    }
}
