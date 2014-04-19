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
            Assert.AreEqual(Pitches.A, this.converter.Parse("A"));
            Assert.AreEqual(Pitches.Asharp, this.converter.Parse("#A"));
            Assert.AreEqual(Pitches.B, this.converter.Parse("B"));
            Assert.AreEqual(Pitches.C, this.converter.Parse("C"));
            Assert.AreEqual(Pitches.Csharp, this.converter.Parse("C#"));
            Assert.AreEqual(Pitches.D, this.converter.Parse("D"));
            Assert.AreEqual(Pitches.Dsharp, this.converter.Parse("D#"));
            Assert.AreEqual(Pitches.E, this.converter.Parse("E"));
            Assert.AreEqual(Pitches.F, this.converter.Parse("F"));
            Assert.AreEqual(Pitches.Fsharp, this.converter.Parse("F#"));
            Assert.AreEqual(Pitches.G, this.converter.Parse("G"));
            Assert.AreEqual(Pitches.Gsharp, this.converter.Parse("G#"));
        }

        [TestMethod]
        public void ParseGoodString_Success()
        {
            // Withspaces
            Assert.AreEqual(Pitches.Csharp, this.converter.Parse(" C# "));

            // Lower case
            Assert.AreEqual(Pitches.D, this.converter.Parse("d"));
            Assert.AreEqual(Pitches.Dsharp, this.converter.Parse("d#"));

            // Withespaces + lower case
            Assert.AreEqual(Pitches.Fsharp, this.converter.Parse(" f# "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseWrongString_Fail()
        {
            Assert.AreNotEqual(Pitches.D, this.converter.Parse(null));
            Assert.AreNotEqual(Pitches.Csharp, this.converter.Parse(string.Empty));
            Assert.AreNotEqual(Pitches.Csharp, this.converter.Parse(""));
            Assert.AreNotEqual(Pitches.Csharp, this.converter.Parse(" "));
            Assert.AreNotEqual(Pitches.A, this.converter.Parse("1"));
        }
    }
}
