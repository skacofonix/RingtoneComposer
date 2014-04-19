﻿using System;
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

        public override string TuneElementPattern
        {
            get { return @"([1|2|4|8|16|32]?)(P|#?[A|C|D|F|G]#?|B|E)(\.?)([4-7]?)"; }
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
                var pitch = PitchConverter.Parse(pitchString);
                var scale = ScaleConverter.Parse(scaleString);

                TuneElement tuneElement;
                if (pitch == Pitchs.P)
                    tuneElement = new Pause(duration);
                else
                    tuneElement = new Note(pitch, scale, duration);
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
                t.Tempo.ToString(CultureInfo.InvariantCulture));

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
    }
}
