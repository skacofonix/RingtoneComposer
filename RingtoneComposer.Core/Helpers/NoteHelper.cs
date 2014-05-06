using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Helpers
{
    public static class NoteHelper
    {
        public static Note Increase(this Note n)
        {
            Note newNote = n;

            if(n.Pitch != Pitches.B)
            {
                newNote.Pitch = IncreasePitch(n.Pitch);
            }
            else if(n.Scale != Scales.Seven)
            {
                newNote.Pitch = Pitches.C;
                newNote.Scale = n.Scale.Increase();
            }

            return newNote;
        }

        private static Pitches IncreasePitch(Pitches p)
        {
            return (Pitches)((int)p + 1 % 12);
        }

        public static Note Decrease(this Note n)
        {
            Note newNote = n;

            if (n.Pitch != Pitches.C)
            {
                newNote.Pitch = DecreasePitch(n.Pitch);
            }
            else if (n.Scale != Scales.Four)
            {
                newNote.Pitch = Pitches.B;
                newNote.Scale = n.Scale.Decrease();
            }

            return newNote;
        }

        private static Pitches DecreasePitch(Pitches p)
        {
            return (Pitches)((int)p - 1 % 12);
        }
    }
}
