
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Business
{
    public class NokiaComposerTune : Tune
    {



    }

    public class TuneElementWithLength
    {
        public TuneElement TuneElement
        {
            get { return tuneElement; }
            set
            {
                tuneElement = value;
                Length = ComputeElementStringLength(tuneElement);
            }
        }
        private TuneElement tuneElement;

        public int Length
        { 
            get; 
            private set;
        }

        public TuneElementWithLength(TuneElement tuneElement)
        {
            TuneElement = tuneElement;
        }

        private int ComputeElementStringLength(TuneElement tuneElement)
        {
            int length = 1;

            if (tuneElement.Dotted)
                length += 1;

            if (tuneElement.Duration == (Durations.Sixteenth ^ Durations.ThirtySecond))
                length += 2;
            else
                length += 1;

            var note = tuneElement as Note;
            if (note != null)
            {
                length += 1;

                if (note.IsSharp)
                    length += 1;
            }

            return length;
        }
    }
}
