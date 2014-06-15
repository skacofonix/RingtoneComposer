using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ComputeLength_Success()
        {
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.Asharp, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.B, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.C, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.Csharp, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.D, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.Dsharp, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.E, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.F, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.Fsharp, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.G, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.Gsharp, Scales.Four, Durations.Quarter)).Length);

            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Five, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Six, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Seven, Durations.Quarter)).Length);

            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Whole)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Half)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Eight)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Sixteenth)).Length);
            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.ThirtySecond)).Length);

            Assert.AreEqual(4, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.Whole) { Dotted = true }).Length);
            Assert.AreEqual(5, new NokiaComposerTuneElementWithLength(new Note(Pitches.A, Scales.Four, Durations.ThirtySecond) { Dotted = true }).Length);

            Assert.AreEqual(2, new NokiaComposerTuneElementWithLength(new Pause(Durations.Quarter)).Length);
            Assert.AreEqual(3, new NokiaComposerTuneElementWithLength(new Pause(Durations.Quarter) { Dotted = true }).Length);
        }
    }
}
