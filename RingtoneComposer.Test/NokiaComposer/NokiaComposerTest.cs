using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class NokiaComposerTest
    {
        private NokiaComposer composer = new NokiaComposer();

        [TestMethod]
        public void PutCharOutOfRangeFail()
        {
            for (int asciiCode = ' '; asciiCode < '~'; asciiCode++)
            {
                if ((asciiCode >= '0' && asciiCode <= '9') || asciiCode == '#' || asciiCode == '*')
                    continue;

                char c = (char)asciiCode;
                try
                {
                    composer.PutChar(c);
                    Assert.Fail("PutChar must be throw exception!");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // Not very clean :-/
                    Assert.IsTrue(ex.Message.Contains("c parameter expected in range 0 to 9 or * or #"));
                }
            }
        }

        [TestMethod]
        public void PutCharInRangeSucess()
        {
            var totalLength = 0;

            composer.PutChar('1');
            Assert.AreEqual(new Note(Pitches.C, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 3, composer.Length);

            composer.PutChar('2');
            Assert.AreEqual(new Note(Pitches.D, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('3');
            Assert.AreEqual(new Note(Pitches.E, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('4');
            Assert.AreEqual(new Note(Pitches.F, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('5');
            Assert.AreEqual(new Note(Pitches.G, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('6');
            Assert.AreEqual(new Note(Pitches.A, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('7');
            Assert.AreEqual(new Note(Pitches.B, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('1');
            composer.PutChar('#');
            Assert.AreEqual(new Note(Pitches.Csharp, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('2');
            composer.PutChar('#');
            Assert.AreEqual(new Note(Pitches.Dsharp, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('4');
            composer.PutChar('#');
            Assert.AreEqual(new Note(Pitches.Fsharp, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('5');
            composer.PutChar('#');
            Assert.AreEqual(new Note(Pitches.Gsharp, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('6');
            composer.PutChar('#');
            Assert.AreEqual(new Note(Pitches.Asharp, Scales.Four, Durations.Quarter), composer.CurrentTuneElement);
            Assert.AreEqual(totalLength += 5, composer.Length);
        }
    }
}
