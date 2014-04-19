namespace RingtoneComposer.Core
{
    public abstract class TuneElement
    {
        public Durations Duration { get; set; }

        public bool Dotted { get; set; }  

        protected TuneElement(Durations duration = Durations.Half, bool dotted = false)
        {
            Duration = duration;
            Dotted = dotted;
        }
    }
}
