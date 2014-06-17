using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Converter
{
    public class PitchKeypressConverter : IConverter<Pitches>
    {
        private const string Key1 = "1";
        private const string Key2 = "2";
        private const string Key3 = "3";
        private const string Key4 = "4";
        private const string Key5 = "5";
        private const string Key6 = "6";
        private const string Key7 = "7";

        public string ToString(Pitches p)
        {
            string pitchString;
            switch (p)
            {
                case Pitches.A:
                case Pitches.Asharp:
                    pitchString = Key6;
                    break;
                case Pitches.B:
                    pitchString = Key7;
                    break;
                case Pitches.C:
                case Pitches.Csharp:
                    pitchString = Key1;
                    break;
                case Pitches.D:
                case Pitches.Dsharp:
                    pitchString = Key2;
                    break;
                case Pitches.E:
                    pitchString = Key3;
                    break;
                case Pitches.F:
                case Pitches.Fsharp:
                    pitchString = Key4;
                    break;
                case Pitches.G:
                case Pitches.Gsharp:
                    pitchString = Key5;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return pitchString;
        }

        public Pitches Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            Pitches pitch;
            switch (s)
            {
                case Key1:
                    pitch = Pitches.C;
                    break;
                case Key2:
                    pitch = Pitches.D;
                    break;
                case Key3:
                    pitch = Pitches.E;
                    break;
                case Key4:
                    pitch = Pitches.F;
                    break;
                case Key5:
                    pitch = Pitches.G;
                    break;
                case Key6:
                    pitch = Pitches.A;
                    break;
                case Key7:
                    pitch = Pitches.B;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return pitch;
        }
    }
}
