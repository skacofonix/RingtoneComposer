namespace RingtoneComposer.Core
{
    public class NoteFrequence
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
