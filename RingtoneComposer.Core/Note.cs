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
    }
}
