using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class PitchConverterTest
    {
        private PitchConverter converter = new PitchConverter();

        [TestMethod]
        public void ToString_Success()
        {
            Assert.AreEqual("A", converter.ToString(Pitches.A));
            Assert.AreEqual("A#", converter.ToString(Pitches.Asharp));
            Assert.AreEqual("B", converter.ToString(Pitches.B));
            Assert.AreEqual("C", converter.ToString(Pitches.C));
            Assert.AreEqual("C#", converter.ToString(Pitches.Csharp));
            Assert.AreEqual("D", converter.ToString(Pitches.D));
            Assert.AreEqual("D#", converter.ToString(Pitches.Dsharp));
            Assert.AreEqual("E", converter.ToString(Pitches.E));
            Assert.AreEqual("F", converter.ToString(Pitches.F));
            Assert.AreEqual("F#", converter.ToString(Pitches.Fsharp));
            Assert.AreEqual("G", converter.ToString(Pitches.G));
            Assert.AreEqual("G#", converter.ToString(Pitches.Gsharp));
        }
    }
}
