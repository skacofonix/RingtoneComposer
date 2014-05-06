using RingtoneComposer.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RingtoneComposer.Core.Converter
{
    public class NokiaKeypressConverter : TuneConverter
    {
        public override string TuneElementPattern
        {
            get { return @"(\()([1-7])(\))|([0-9*#])"; }
        }

        public override string TuneElementDelimiter
        {
            get { return " "; }
        }

        public override string Pause
        {
            get { return "0"; }
        }

        public override int DefaultBpm
        {
            get { return 120; }
        }

        const Durations DefaultDuration = Durations.Eight;
        const Scales DefaultScale = Scales.Four;
        const string OpenBracket = "(";
        const string CloseBracket = ")";
        const string KeyPause = "0";
        const string KeyC = "1";
        const string KeyD = "2";
        const string KeyE = "3";
        const string KeyF = "4";
        const string KeyG = "5";
        const string KeyA = "6";
        const string KeyB = "7";
        const string KeyDecreaseDuration = "8";
        const string KeyIncreaseDuration = "9";
        const string KeyIncreaseScale = "*";
        const string KeyToggleSharp = "#";

        protected override string InternalToString(Tune t)
        {
            Durations previousDuration = DefaultDuration;
            Scales previousScale = DefaultScale;

            var sb = new StringBuilder();

            foreach (var tuneElement in t.TuneElementList)
            {
                if (tuneElement.Dotted)
                    sb.Append(OpenBracket);

                AppendNoteOrPause(sb, tuneElement);

                if (tuneElement.Dotted)
                    sb.Append(CloseBracket);

                previousDuration = AppendDurationKey(sb, tuneElement, previousDuration);

                AppendSharpKey(sb, tuneElement);

                previousScale = AppendScaleKey(sb, tuneElement, previousScale);

                sb.Append(TuneElementDelimiter);
            }

            return sb.ToString().TrimEnd();
        }

        private static void AppendNoteOrPause(StringBuilder sb, TuneElement tuneElement)
        {
            var note = tuneElement as Note;
            if (note != null)
                sb.Append(ConvertPitchToKey(note));
            else if (tuneElement is Pause)
                sb.Append(KeyPause);
        }

        private static void AppendSharpKey(StringBuilder sb, TuneElement tuneElement)
        {
            var note = tuneElement as Note;
            if (note != null && note.IsSharp)
                sb.Append(KeyToggleSharp);
        }

        private static Durations AppendDurationKey(StringBuilder sb, TuneElement tuneElement, Durations previousDuration)
        {
            if (tuneElement.Duration != previousDuration)
            {
                var currentDuration = previousDuration;

                do
                {
                    if (currentDuration > tuneElement.Duration)
                    {
                        currentDuration = currentDuration.Increase();
                        sb.Append(KeyIncreaseDuration);
                    }
                    else
                    {
                        currentDuration = currentDuration.Decrease();
                        sb.Append(KeyDecreaseDuration);
                    }
                } while (currentDuration != tuneElement.Duration);

                previousDuration = currentDuration;
            }
            return previousDuration;
        }

        private static Scales AppendScaleKey(StringBuilder sb, TuneElement tuneElement, Scales previousScale)
        {
            var note = tuneElement as Note;
            if (note != null && note.Scale != previousScale)
            {
                var currentScale = previousScale;

                do
                {
                    currentScale = currentScale.Increase();
                    sb.Append(KeyIncreaseScale);
                } while (currentScale != note.Scale);

                previousScale = currentScale;
            }
            return previousScale;
        }

        private static string ConvertPitchToKey(Note note)
        {
            string pitchString;
            switch (note.Pitch)
            {
                case Pitches.A:
                case Pitches.Asharp:
                    pitchString = KeyA;
                    break;
                case Pitches.B:
                    pitchString = KeyB;
                    break;
                case Pitches.C:
                case Pitches.Csharp:
                    pitchString = KeyC;
                    break;
                case Pitches.D:
                case Pitches.Dsharp:
                    pitchString = KeyD;
                    break;
                case Pitches.E:
                    pitchString = KeyE;
                    break;
                case Pitches.F:
                case Pitches.Fsharp:
                    pitchString = KeyF;
                    break;
                case Pitches.G:
                case Pitches.Gsharp:
                    pitchString = KeyG;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Pitch");
            }
            return pitchString;
        }

        protected override Tune InternalParse(string s)
        {
            Durations previousDuration = DefaultDuration;
            Scales previousScale = DefaultScale;
            TuneElement currentTuneElement = null;
            var tuneElementList = new List<TuneElement>();

            var regex = new Regex(TuneElementPattern);

            foreach (Match item in regex.Matches(s))
            {
                bool dotted = false;
                string key = string.Empty;
                ReadKey(item, ref dotted, ref key);

                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("key");

                var currentNote = currentTuneElement as Note;
                switch (key)
                {
                    case KeyA:
                    case KeyB:
                    case KeyC:
                    case KeyD:
                    case KeyE:
                    case KeyF:
                    case KeyG:
                        if (currentTuneElement != null)
                            tuneElementList.Add(currentTuneElement);

                        var pitch = ConvertKeyToPitch(key);
                        currentTuneElement = new Note(pitch, previousScale, previousDuration);
                        currentTuneElement.Dotted = dotted;
                        break;

                    case KeyPause:
                        currentTuneElement = new Pause(previousDuration);
                        currentTuneElement.Dotted = dotted;
                        break;

                    case KeyDecreaseDuration:
                        previousDuration = previousDuration.Decrease();
                        currentTuneElement.Duration = previousDuration;
                        break;

                    case KeyIncreaseDuration:
                        previousDuration = previousDuration.Increase();
                        currentTuneElement.Duration = previousDuration;
                        break;

                    case KeyIncreaseScale:
                        if (currentNote != null)
                            currentNote.Scale = currentNote.Scale.Increase();
                        break;

                    case KeyToggleSharp:
                        if (currentNote != null)
                            ToggleSharpNote(currentNote);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(key);
                }
            }

            tuneElementList.Add(currentTuneElement);

            var tune = new Tune(tuneElementList);

            return tune;
        }

        private static void ReadKey(Match item, ref bool dotted, ref string key)
        {
            if (item.Groups[1].Value.Equals(OpenBracket) && item.Groups[3].Value.Equals(CloseBracket))
            {
                key = item.Groups[2].Value;
                dotted = true;
            }
            else
            {
                key = item.Groups[4].Value;
                dotted = false;
            }
        }

        private static Pitches ConvertKeyToPitch(string key)
        {
            Pitches pitch;
            switch (key)
            {
                case "1":
                    pitch = Pitches.C;
                    break;
                case "2":
                    pitch = Pitches.D;
                    break;
                case "3":
                    pitch = Pitches.E;
                    break;
                case "4":
                    pitch = Pitches.F;
                    break;
                case "5":
                    pitch = Pitches.G;
                    break;
                case "6":
                    pitch = Pitches.A;
                    break;
                case "7":
                    pitch = Pitches.B;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(key);
            }
            return pitch;
        }

        private static void ToggleSharpNote(Note currentNote)
        {
            if (currentNote.IsSharpable)
            {
                var pitchArray = new Dictionary<Pitches, Pitches>()
                            {
                                {Pitches.A, Pitches.Asharp},
                                {Pitches.C, Pitches.Csharp},
                                {Pitches.D, Pitches.Dsharp},
                                {Pitches.F, Pitches.Fsharp},
                                {Pitches.G, Pitches.Gsharp}
                            };

                if (currentNote.IsSharp)
                    currentNote.Pitch = pitchArray.SingleOrDefault(pitch => pitch.Value.Equals(currentNote.Pitch)).Key;
                else
                    currentNote.Pitch = pitchArray[currentNote.Pitch];
            }
        }
    }
}
