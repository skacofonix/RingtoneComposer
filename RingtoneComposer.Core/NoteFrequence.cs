namespace RingtoneComposer.Core
{
    public class NoteFrequence
    {
        public Pitchs Pitch { get; private set; }
        public Scales Scale { get; private set; }
        public double Frequence { get; private set; }
        
        public NoteFrequence(Pitchs pitch, Scales scale, double frequence)
        {
            Pitch = pitch;
            Scale = scale;
            Frequence = frequence;
        }
    }
}
