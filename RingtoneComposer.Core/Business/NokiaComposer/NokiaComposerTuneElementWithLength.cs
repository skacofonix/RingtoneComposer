
namespace RingtoneComposer.Core
{
    public class NokiaComposerTuneElementWithLength
    {
        public TuneElement TuneElement
        {
            get { return tuneElement; }
            set
            {
                tuneElement = value;
                ComputeElementStringLength();
            }
        }
        private TuneElement tuneElement;

        public int Length
        {
            get;
            private set;
        }

        public NokiaComposerTuneElementWithLength(TuneElement tuneElement)
        {
            TuneElement = tuneElement;
        }

        public void ComputeElementStringLength()
        {
            int length = 1;

            if (tuneElement.Dotted)
                length += 1;

            if (tuneElement.Duration == Durations.Sixteenth || tuneElement.Duration == Durations.ThirtySecond)
                length += 2;
            else
                length += 1;

            var note = tuneElement as Note;
            if (note != null)
            {
                length += 1;    // Scale

                if (note.IsSharp)
                    length += 1;
            }

            Length = length;
        }
    }
}
