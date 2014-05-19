﻿using RingtoneComposer.Core.Helpers;
using System;
namespace RingtoneComposer.Core
{
    public class Note : TuneElement
    {
        public Pitches Pitch { get; set; }
        
        public Scales Scale { get; set; }

        public bool IsSharpable
        {
            get
            {
                if (Pitch == Pitches.B || Pitch == Pitches.E)
                    return false;
                return true;
            }
        }

        public bool IsSharp 
        {
            get
            {
                switch(Pitch)
                {
                    case Pitches.Asharp:
                    case Pitches.Csharp:
                    case Pitches.Dsharp:
                    case Pitches.Fsharp:
                    case Pitches.Gsharp:
                        return true;
                    default :
                        return false;
                }
            }
        }

        public Note()
        { }

        public Note(Pitches pitch, Scales scale, Durations duration)
            :base(duration)
        {
            Pitch = pitch;
            Scale = scale;  
        }

        public bool IncreasePitch()
        {
            bool isSuccess = false;

            if (Pitch != Pitches.B)
            {
                Pitch = IncreasePitch(Pitch);
                isSuccess = true;
            }
            else if (Scale != Scales.Seven)
            {
                Pitch = Pitches.C;
                Scale = ScaleHelper.Increase(Scale);
                isSuccess = true;
            }

            return isSuccess;
        }

        private Pitches IncreasePitch(Pitches p)
        {
            return (Pitches)((int)p + 1 % 12);
        }

        public bool DecreasePitch()
        {
            bool isSuccess = false;

            if (Pitch != Pitches.C)
            {
                Pitch = DecreasePitch(Pitch);
                isSuccess = true;
            }
            else if (Scale != Scales.Four)
            {
                Pitch = Pitches.B;
                Scale = ScaleHelper.Decrease(Scale);
                isSuccess = true;
            }

            return isSuccess;
        }

        private Pitches DecreasePitch(Pitches p)
        {
            return (Pitches)((int)p - 1 % 12);
        }

        public bool IncreaseScale()
        {
            var newScale = ScaleHelper.Increase(Scale);

            bool isSuccess = Scale != newScale;

            if (isSuccess)
                Scale = newScale;

            return isSuccess;
        }

        public bool DecreaseScale()
        {
            var newScale = ScaleHelper.Decrease(Scale);

            bool isSuccess = Scale != newScale;

            if (isSuccess)
                Scale = newScale;

            return isSuccess;
        }
    }
}