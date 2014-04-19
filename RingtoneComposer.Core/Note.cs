namespace RingtoneComposer.Core
{
    public class Note : TuneElement
    {
        public Pitchs Pitch { get; set; }
        public Scales Scale { get; set; }

        public Note()
        { }

        public Note(Pitchs pitch, Scales scale, Durations duration)
            :base(duration)
        {
            Pitch = pitch;
            Scale = scale;  
        }
    }
}
