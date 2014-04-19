using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class SoundGeneratorTest
    {
        [TestMethod]
        public void SoundGeneratorSimpleTest()
        {
            const int sampleRate = 44100;

            var tune = new Tune();
            var oscillator = new SineOscillator(sampleRate);
            var frequenceRepository = new FrequenceRepository();

            var A440 = frequenceRepository[Pitchs.A, Scales.Four];

            var data = new List<double>();
            oscillator.SetFrequency(A440);
            for (int i = 0; i < sampleRate; i++)
                data.Add(oscillator.GetNext(i));

            //SoundGenerator.(data.ToArray(), data.Count, sampleRate);

        }
    }
}
