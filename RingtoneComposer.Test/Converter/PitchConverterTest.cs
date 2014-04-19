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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Fail1()
        {
            converter.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_Fail2()
        {
            converter.Parse(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_Fail3()
        {
            converter.Parse(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_Fail4()
        {
            converter.Parse("1");
        }

        [TestMethod]
        public void Parse_Success()
        {
            // Withspaces
            Assert.AreEqual(Pitches.Csharp, this.converter.Parse(" C# "));

            // Lower case
            Assert.AreEqual(Pitches.D, this.converter.Parse("d"));
            Assert.AreEqual(Pitches.Dsharp, this.converter.Parse("d#"));

            // Withespaces + lower case
            Assert.AreEqual(Pitches.Fsharp, this.converter.Parse(" f# "));

            // All values
            Assert.AreEqual(Pitches.A, this.converter.Parse("A"));
            Assert.AreEqual(Pitches.Asharp, this.converter.Parse("A#"));
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
    }
}
