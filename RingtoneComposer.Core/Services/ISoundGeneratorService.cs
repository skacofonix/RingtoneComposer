using System.IO;

namespace RingtoneComposer.Core.Services
{
    public interface ISoundGeneratorService
    {
        Stream TuneToWaveStream(Tune tune);

        Stream TuneToMp3Stream(Tune tune);
    }
}
