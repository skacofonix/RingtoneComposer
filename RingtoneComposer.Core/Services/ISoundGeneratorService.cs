using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public interface ISoundGeneratorService
    {
        Stream TuneToWaveStream(Tune tune);

        Stream TuneToMp3Stream(Tune tune);
    }
}
