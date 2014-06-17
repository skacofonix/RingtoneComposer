using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class NokiaComposerTuneElementListTest
    {
        private NokiaComposerTuneElementList list = new NokiaComposerTuneElementList();

        [TestMethod]
        public void AddTuneElement()
        {
            Assert.AreEqual(0, list.Length);

            int totalLength = 0;

            {
                var tuneElement = new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Quarter));
                list.Add(tuneElement);
                totalLength += tuneElement.Length;
                Assert.AreEqual(totalLength, list.Length);
            }
            {
                var tuneElement = new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Half));
                list.Add(tuneElement);
                totalLength += tuneElement.Length + 1;
                Assert.AreEqual(totalLength, list.Length);
            }
            {
                var tuneElement = new NokiaComposerTuneElementWithLength(new Pause(Durations.Quarter));
                list.Add(tuneElement);
                totalLength += tuneElement.Length + 1;
                Assert.AreEqual(totalLength, list.Length);
            }
            {
                var tuneElement = new NokiaComposerTuneElementWithLength(new Pause(Durations.Quarter) { Dotted = true });
                list.Add(tuneElement);
                totalLength += tuneElement.Length + 1;
                Assert.AreEqual(totalLength, list.Length);
            }
            {
                var tuneElement = new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Whole) { Dotted = true });
                list.Add(tuneElement);
                totalLength += tuneElement.Length + 1;
                Assert.AreEqual(totalLength, list.Length);
            }
            {
                var tuneElement = new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.ThirtySecond) { Dotted = true });
                list.Add(tuneElement);
                totalLength += tuneElement.Length + 1;
                Assert.AreEqual(totalLength, list.Length);
            }
        }
    }
}
