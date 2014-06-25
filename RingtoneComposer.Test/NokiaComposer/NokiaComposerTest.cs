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
        public void PutCharInRangeWitthPitchDoSucess()
        {
            var totalLength = 0;

            // Pitch
            composer.PutChar('1');
            Assert.IsTrue(new Note(Pitches.C, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 3, composer.Length);

            composer.PutChar('2');
            Assert.IsTrue(new Note(Pitches.D, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('3');
            Assert.IsTrue(new Note(Pitches.E, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('4');
            Assert.IsTrue(new Note(Pitches.F, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('5');
            Assert.IsTrue(new Note(Pitches.G, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('6');
            Assert.IsTrue(new Note(Pitches.A, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('7');
            Assert.IsTrue(new Note(Pitches.B, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);
        }

        [TestMethod]
        public void PutCharInRangeWitthScaleDoSucess()
        {
            var totalLength = 0;

            composer.PutChar('1');
            composer.PutChar('*');
            Assert.IsTrue(new Note(Pitches.C, Scales.Five, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 3, composer.Length);

            composer.PutChar('2');
            composer.PutChar('*');
            Assert.IsTrue(new Note(Pitches.D, Scales.Six, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('3');
            composer.PutChar('*');
            Assert.IsTrue(new Note(Pitches.E, Scales.Seven, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('4');
            composer.PutChar('*');
            Assert.IsTrue(new Note(Pitches.F, Scales.Four, Durations.Quarter).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);
        }

        [TestMethod]
        public void PutCharInRangeWitthDurationDoSucess()
        {
            var totalLength = 0;

            composer.PutChar('1');
            composer.PutChar('8');
            Assert.IsTrue(new Note(Pitches.C, Scales.Four, Durations.Eight).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 3, composer.Length);

            composer.PutChar('2');
            composer.PutChar('8');
            Assert.IsTrue(new Note(Pitches.D, Scales.Four, Durations.Sixteenth).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('3');
            composer.PutChar('8');
            Assert.IsTrue(new Note(Pitches.E, Scales.Four, Durations.ThirtySecond).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('4');
            composer.PutChar('8');
            Assert.IsTrue(new Note(Pitches.F, Scales.Four, Durations.ThirtySecond).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('5');
            composer.PutChar('9');
            Assert.IsTrue(new Note(Pitches.G, Scales.Four, Durations.Sixteenth).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 5, composer.Length);

            composer.PutChar('6');
            composer.PutChar('9');
            composer.PutChar('9');
            composer.PutChar('9');
            composer.PutChar('9');
            Assert.IsTrue(new Note(Pitches.A, Scales.Four, Durations.Whole).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('7');
            composer.PutChar('9');
            Assert.IsTrue(new Note(Pitches.B, Scales.Four, Durations.Whole).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);

            composer.PutChar('7');
            composer.PutChar('8');
            Assert.IsTrue(new Note(Pitches.B, Scales.Four, Durations.Half).Equals(composer.CurrentTuneElement));
            Assert.AreEqual(totalLength += 4, composer.Length);
        }

        [TestMethod]
        public void PutCharInRangeDottedDoSuccess()
        {
            Assert.Fail("Not yet implemented, coming soon!");
        }
    }
}
