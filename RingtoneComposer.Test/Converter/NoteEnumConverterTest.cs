using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class NoteEnumConverterTest
    {
        private PitchConverter converter;

        public NoteEnumConverterTest()
        {
            converter = new PitchConverter();
        }

        [TestMethod]
        public void ParseExactString_Success()
        {
            Assert.AreEqual(Pitchs.A, this.converter.Parse("A"));
            Assert.AreEqual(Pitchs.Asharp, this.converter.Parse("#A"));
            Assert.AreEqual(Pitchs.B, this.converter.Parse("B"));
            Assert.AreEqual(Pitchs.C, this.converter.Parse("C"));
            Assert.AreEqual(Pitchs.Csharp, this.converter.Parse("C#"));
            Assert.AreEqual(Pitchs.D, this.converter.Parse("D"));
            Assert.AreEqual(Pitchs.Dsharp, this.converter.Parse("D#"));
            Assert.AreEqual(Pitchs.E, this.converter.Parse("E"));
            Assert.AreEqual(Pitchs.F, this.converter.Parse("F"));
            Assert.AreEqual(Pitchs.Fsharp, this.converter.Parse("F#"));
            Assert.AreEqual(Pitchs.G, this.converter.Parse("G"));
            Assert.AreEqual(Pitchs.Gsharp, this.converter.Parse("G#"));
        }

        [TestMethod]
        public void ParseGoodString_Success()
        {
            // Withspaces
            Assert.AreEqual(Pitchs.Csharp, this.converter.Parse(" C# "));

            // Lower case
            Assert.AreEqual(Pitchs.D, this.converter.Parse("d"));
            Assert.AreEqual(Pitchs.Dsharp, this.converter.Parse("d#"));

            // Withespaces + lower case
            Assert.AreEqual(Pitchs.Fsharp, this.converter.Parse(" f# "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseWrongString_Fail()
        {
            Assert.AreNotEqual(Pitchs.D, this.converter.Parse(null));
            Assert.AreNotEqual(Pitchs.Csharp, this.converter.Parse(string.Empty));
            Assert.AreNotEqual(Pitchs.Csharp, this.converter.Parse(""));
            Assert.AreNotEqual(Pitchs.Csharp, this.converter.Parse(" "));
            Assert.AreNotEqual(Pitchs.A, this.converter.Parse("1"));
        }
    }
}
