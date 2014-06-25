using System;

namespace RingtoneComposer.Core.Services
{
    public class SoundGeneratorService : ISoundGeneratorService
    {
        public System.IO.Stream TuneToWaveStream(Tune tune)
        {
            return soundGenerator.TuneToWaveStream(tune);
        }

        public System.IO.Stream TuneToMp3Stream(Tune tune)
        {
            throw new NotImplementedException();
        }

        private SoundGenerator soundGenerator = null;

        public SoundGeneratorService()
        {
            soundGenerator = new SoundGenerator(new SquareOscilator(44100));
        }
    }
}
