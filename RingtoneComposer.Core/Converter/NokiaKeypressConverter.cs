using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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


        const Durations defaultDuration = Durations.Quarter;
        const Scales defaultScale = Scales.Four;
        const string OpenBracket = "(";
        const string CloseBracket = ")";

        protected override string InternalToString(Tune t)
        {
            throw new NotImplementedException();
        }

        protected override Tune InternalParse(string s)
        {
            Durations previousDuration = defaultDuration;
            Scales previousScale = defaultScale;
            Pitches? previousPitch = null;
            TuneElement currentTuneElement = null;

            var tuneList = new List<TuneElement>();

            var regex = new Regex(TuneElementPattern);
            foreach (Match item in regex.Matches(s))
            {
                bool dotted = false;
                string key = string.Empty;
                if(item.Groups[1].Value.Equals(OpenBracket) && item.Groups[3].Value.Equals(CloseBracket))
                {
                    key = item.Groups[2].Value;
                    dotted = true;
                }
                else
                {
                    key = item.Groups[4].Value;
                    dotted = false;
                }

                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("key");

                char c = key.ToCharArray(0, 1).First();
                if (c >= '1' && c <= '7')
                {
                    if(currentTuneElement != null)
                         tuneList.Add(currentTuneElement);

                    var pitch = ConvertKeyToPitch(key);

                    currentTuneElement = new Note(pitch, previousScale, previousDuration);
                    currentTuneElement.Dotted = dotted;
                }
                else if (c == '0')
                {
                    currentTuneElement = new Pause(previousDuration);
                }
                else if(c == '8')
                {
                    currentTuneElement.Duration = DecreaseDuration(currentTuneElement.Duration);
                }
                else if(c == '9')
                {
                    currentTuneElement.Duration = IncreaseDuration(currentTuneElement.Duration);
                }
                else if(c == '*')
                {
                    var currentNote = currentTuneElement as Note;
                    if(currentNote != null)
                        currentNote.Scale = IncreaseScale(currentNote.Scale);
                }
                else if(c == '#')
                {
                    var currentNote = currentTuneElement as Note;
                    if(currentNote != null)
                    {
                        if(currentNote.IsSharpable)
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

            tuneList.Add(currentTuneElement);

            Tune tune = new Tune(tuneList);

            return tune;
        }

        private static Pitches ConvertKeyToPitch(string key)
        {
            Pitches pitch;
            switch(key)
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
                    throw new ArgumentOutOfRangeException();
            }
            return pitch;
        }

        private static Durations IncreaseDuration(Durations d)
        {
            Durations newDuration;
            if (d == Durations.Whole)
                newDuration = Durations.Whole;
            else
                newDuration = (Durations)((int)d / 2);
            return newDuration;
            //var value = (int)d;
            //if (value == 1)
            //    value = 32;
            //return (Durations)(value / 2);
        }

        private static Durations DecreaseDuration(Durations d)
        {
            Durations newDuration;
            if (d == Durations.ThirtySecond)
                newDuration = Durations.ThirtySecond;
            else
                newDuration = (Durations)((int)d * 2);
            //return (Durations)((int)d * 2 % 32);
        }

        private static Scales IncreaseScale(Scales s)
        {
            return (Scales)((int)s-4+1%4+4);
        }
    }
}
