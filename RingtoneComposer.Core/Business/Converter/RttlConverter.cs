using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RingtoneComposer.Core.Converter
{
    public class RttlConverter : TuneConverter
    {
        private const char TuneCommandDelimiter = ':';
        private const string PatternDefaultBpm = @"b=([0-9]{1,3})";
        private const string PatternDefaultDuration = @"d=(1|2|4|8|16|32)";
        private const string PatternDefaultScale = @"o=([4-7])";
        private const string DefaultDuration = "4";
        private const string DefaultScale = "6";

        public override string PartitionValidatorPattern
        {
            get
            {
                //var name = @"(?!:)\w{0,10}";
                //var controlSection = @"(b=([0-9]{1,3}),?){0,1}|(d=(1|2|4|8|16|32),?){0,1}|(o=([4-7]),?){0,1}";
                //var toneComands = @"(1|2|4|8|16|32)?(P|#?[ACDFG]#?|B|E)(\.?)([4-7]?)";

                var rttlPattern = @"(?!:)\w{0,10}:(((b=([0-9]{1,3}),?){0,1}|(d=(1|2|4|8|16|32),?){0,1}|(o=([4-7]),?){0,1}):)?(((1|2|4|8|16|32)?(P|#?[acdfg]#?|b|e)(\.?)([4-7]?))[\s,]?){1,50}";

                return rttlPattern;
            }
        }

        public override string TuneElementPattern
        {
            get { return @"(1|2|4|8|16|32)?(P|#?[ACDFG]#?|B|E)(\.?)([4-7]?)"; }
        }

        public override string TuneElementDelimiter
        {
            get { return ","; }
        }

        public override string Pause
        {
            get { return "P"; }
        }

        public override int DefaultBpm
        {
            get { return 63; }
        }

        protected override string InternalToString(Tune t)
        {
            var sb = new StringBuilder();

            sb.Append(t.Name);
            sb.Append(TuneCommandDelimiter);

            var defaultDuration = t.TuneElementList.GroupBy(g => g.Duration)
                                                   .OrderByDescending(o => o.Count())
                                                   .First().Key;

            var defaultScale = t.TuneElementList.OfType<Note>()
                                                .GroupBy(g => g.Scale)
                                                .OrderByDescending(o => o.Count())
                                                .First().Key;

            sb.AppendFormat("d={0},o={1},b={2}",
                DurationConverter.ToString(defaultDuration),
                ScaleConverter.ToString(defaultScale),
                t.Tempo.HasValue ? t.Tempo.Value.ToString(CultureInfo.InvariantCulture) : DefaultBpm.ToString());

            sb.Append(TuneCommandDelimiter);

            foreach (var tuneElement in t.TuneElementList)
            {
                if (tuneElement.Duration != defaultDuration)
                    sb.Append(DurationConverter.ToString(tuneElement.Duration));

                var note = tuneElement as Note;
                if (note != null)
                {
                    sb.Append(PitchConverter.ToString(note.Pitch));
                    if (note.Scale != defaultScale)
                        sb.Append(ScaleConverter.ToString(note.Scale));
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
            var parts = s.Split(TuneCommandDelimiter);
            if (parts.Count() < 2 || parts.Count() > 3)
                throw new FormatException();

            var name = parts[0];

            string notes;
            var bpm = DefaultBpm.ToString();
            var defaultDuration = DefaultDuration;
            var defaultScale = DefaultScale;

            if (parts.Count() == 3)
            {
                notes = parts[2].ToUpperInvariant();

                var commandSection = parts[1];

                var bpmString = GetFirstValueGroup(commandSection, PatternDefaultBpm);
                if (!string.IsNullOrEmpty(bpmString))
                    bpm = bpmString;

                var durationString = GetFirstValueGroup(commandSection, PatternDefaultDuration);
                if (!string.IsNullOrEmpty(durationString))
                    defaultDuration = durationString;

                var scaleString = GetFirstValueGroup(commandSection, PatternDefaultScale);
                if (!string.IsNullOrEmpty(scaleString))
                    defaultScale = scaleString;
            }
            else
            {
                notes = parts[1].ToUpperInvariant();
            }

            var tuneElementList = new List<TuneElement>();
            var regexNotes = new Regex(TuneElementPattern);
            var matches = regexNotes.Matches(notes);
            foreach (Match m in matches)
            {
                var durationString = string.IsNullOrEmpty(m.Groups[1].Value) ? defaultDuration : m.Groups[1].Value;
                var pitchString = m.Groups[2].Value;
                var dotted = m.Groups[3].Value == ".";
                var scaleString = string.IsNullOrEmpty(m.Groups[4].Value) ? defaultScale : m.Groups[4].Value;

                var duration = DurationConverter.Parse(durationString);
                var scale = ScaleConverter.Parse(scaleString);

                TuneElement tuneElement;
                if (pitchString == Pause)
                {
                    tuneElement = new Pause(duration);
                }
                else
                {
                    var pitch = PitchConverter.Parse(pitchString);
                    tuneElement = new Note(pitch, scale, duration);
                }
                tuneElement.Dotted = dotted;
                tuneElementList.Add(tuneElement);
            }

            var tune = new Tune(tuneElementList, Convert.ToInt32(bpm));
            tune.Name = name;

            return tune;
        }

        private static string GetFirstValueGroup(string input, string pattern)
        {
            var regex = new Regex(pattern);
            var matches = regex.Match(input);
            return matches.Success ? matches.Groups[1].Value : null;
        }
    }
}
