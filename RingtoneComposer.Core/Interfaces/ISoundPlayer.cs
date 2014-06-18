using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Interfaces
{
    public interface ISoundPlayer
    {
        void Play(Tune tune);

        void Play(Note note, double period);

        void Stop();
    }
}
