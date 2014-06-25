using RingtoneComposer.Core.Helpers;
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
                return (Pitch != Pitches.B && Pitch != Pitches.E);
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

        public Note(Pitches pitch, Scales scale, Durations duration)
            :base(duration)
        {
            Pitch = pitch;
            Scale = scale;  
        }

        public bool CanIncreasePitch()
        {
            return !(Scale == Scales.Seven && Pitch == Pitches.B);
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

        public bool CanDecreasePitch()
        {
            return !(Scale == Scales.Four && Pitch == Pitches.C);
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

        public bool CanIncreaseScale()
        {
            return Scale != Scales.Seven;
        }

        public bool IncreaseScale()
        {
            var newScale = ScaleHelper.Increase(Scale);

            bool isSuccess = Scale != newScale;

            if (isSuccess)
                Scale = newScale;

            return isSuccess;
        }

        public bool CanDecreaseScale()
        {
            return Scale != Scales.Four;
        }

        public bool DecreaseScale()
        {
            var newScale = ScaleHelper.Decrease(Scale);

            bool isSuccess = Scale != newScale;

            if (isSuccess)
                Scale = newScale;

            return isSuccess;
        }

        public bool IncreaseScaleModulo()
        {
            var newScale = ScaleHelper.IncreaseModulo(Scale);

            bool isSuccess = Scale != newScale;

            if (isSuccess)
                Scale = newScale;

            return isSuccess;
        }

        public bool ToggleSharp()
        {
            if (!IsSharpable)
                return false;

            if (IsSharp)
                return DecreasePitch();
            return IncreasePitch();
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            Note otherElement = obj as Note;

            if (this.Pitch != otherElement.Pitch)
                return false;

            if (this.Scale != otherElement.Scale)
                return false;

            return true;
        }
    }
}
