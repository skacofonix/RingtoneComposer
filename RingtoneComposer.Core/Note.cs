namespace RingtoneComposer.Core
{
    public class Note : TuneElement
    {
        public Pitches Pitch { get; set; }
        public Scales Scale { get; set; }

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
