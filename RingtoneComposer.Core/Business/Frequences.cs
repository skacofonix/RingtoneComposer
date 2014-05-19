using System;
using System.Collections.Generic;
using System.Linq;

namespace RingtoneComposer.Core
{
    public class Frequences
    {
        public double this[Pitches pitch, Scales scale]
        {
            get
            {
                return GetFrequence(pitch, scale);
            }
        }

        private double GetFrequence(Pitches pitch, Scales scale)
        {
            return noteFrequenceArray.Where(w => w.Pitch.Equals(pitch) && w.Scale.Equals(scale))
                                     .Select(s => s.Frequence)
                                     .FirstOrDefault();
        }

        private readonly List<NoteFrequence> noteFrequenceArray = new List<NoteFrequence>
        {
            new NoteFrequence(Pitches.C,Scales.Four,523.25),
            new NoteFrequence(Pitches.Csharp,Scales.Four,554.37),
            new NoteFrequence(Pitches.D,Scales.Four,587.33),
            new NoteFrequence(Pitches.Dsharp,Scales.Four,622.25),
            new NoteFrequence(Pitches.E,Scales.Four,659.26),
            new NoteFrequence(Pitches.F,Scales.Four,698.46),
            new NoteFrequence(Pitches.Fsharp,Scales.Four,739.99),
            new NoteFrequence(Pitches.G,Scales.Four,783.99),
            new NoteFrequence(Pitches.Gsharp,Scales.Four,830.61),
            new NoteFrequence(Pitches.A,Scales.Four,880),
            new NoteFrequence(Pitches.Asharp,Scales.Four,932.33),
            new NoteFrequence(Pitches.B,Scales.Four,987.77),
            new NoteFrequence(Pitches.C,Scales.Five,1046.5),
            new NoteFrequence(Pitches.Csharp,Scales.Five,1108.73),
            new NoteFrequence(Pitches.D,Scales.Five,1174.66),
            new NoteFrequence(Pitches.Dsharp,Scales.Five,1244.51),
            new NoteFrequence(Pitches.E,Scales.Five,1318.51),
            new NoteFrequence(Pitches.F,Scales.Five,1396.91),
            new NoteFrequence(Pitches.Fsharp,Scales.Five,1479.98),
            new NoteFrequence(Pitches.G,Scales.Five,1567.98),
            new NoteFrequence(Pitches.Gsharp,Scales.Five,1661.22),
            new NoteFrequence(Pitches.A,Scales.Five,1760),
            new NoteFrequence(Pitches.Asharp,Scales.Five,1864.66),
            new NoteFrequence(Pitches.B,Scales.Five,1975.53),
            new NoteFrequence(Pitches.C,Scales.Six,2093),
            new NoteFrequence(Pitches.Csharp,Scales.Six,2217.46),
            new NoteFrequence(Pitches.D,Scales.Six,2349.32),
            new NoteFrequence(Pitches.Dsharp,Scales.Six,2489.02),
            new NoteFrequence(Pitches.E,Scales.Six,2637.02),
            new NoteFrequence(Pitches.F,Scales.Six,2793.83),
            new NoteFrequence(Pitches.Fsharp,Scales.Six,2959.96),
            new NoteFrequence(Pitches.G,Scales.Six,3135.96),
            new NoteFrequence(Pitches.Gsharp,Scales.Six,3322.44),
            new NoteFrequence(Pitches.A,Scales.Six,3520),
            new NoteFrequence(Pitches.Asharp,Scales.Six,3729.31),
            new NoteFrequence(Pitches.B,Scales.Six,3951.07),
            new NoteFrequence(Pitches.C,Scales.Seven,4186.01),
            new NoteFrequence(Pitches.Csharp,Scales.Seven,4434.92),
            new NoteFrequence(Pitches.D,Scales.Seven,4698.64),
            new NoteFrequence(Pitches.Dsharp,Scales.Seven,4978.03),
            new NoteFrequence(Pitches.E,Scales.Seven,5274.04),
            new NoteFrequence(Pitches.F,Scales.Seven,5587.65),
            new NoteFrequence(Pitches.Fsharp,Scales.Seven,5919.91),
            new NoteFrequence(Pitches.G,Scales.Seven,6271.93),
            new NoteFrequence(Pitches.Gsharp,Scales.Seven,6644.88),
            new NoteFrequence(Pitches.A,Scales.Seven,7040),
            new NoteFrequence(Pitches.Asharp,Scales.Seven,7458.62),
            new NoteFrequence(Pitches.B,Scales.Seven,7902.13)
        };

        private class NoteFrequence
        {
            public Pitches Pitch { get; private set; }
            public Scales Scale { get; private set; }
            public double Frequence { get; private set; }

            public NoteFrequence(Pitches pitch, Scales scale, double frequence)
            {
                Pitch = pitch;
                Scale = scale;
                Frequence = frequence;
            }
        }
    }
}
