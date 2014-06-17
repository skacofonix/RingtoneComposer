using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class PitchKeypressConverterTest
    {
        private PitchKeypressConverter converter = new PitchKeypressConverter();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseNullParameterDoFail()
        {
            converter.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseStringEmptyDoFail()
        {
            converter.Parse(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseOutOfRangeKeyPressDoFail()
        {
            converter.Parse("A");
        }

        [TestMethod]
        public void ParseValidKeypressDoSuccess()
        {
            Assert.AreEqual(Pitches.C, converter.Parse("1"));
            Assert.AreEqual(Pitches.D, converter.Parse("2"));
            Assert.AreEqual(Pitches.E, converter.Parse("3"));
            Assert.AreEqual(Pitches.F, converter.Parse("4"));
            Assert.AreEqual(Pitches.G, converter.Parse("5"));
            Assert.AreEqual(Pitches.A, converter.Parse("6"));
            Assert.AreEqual(Pitches.B, converter.Parse("7"));
        }

        [TestMethod]
        public void ToStringDoSuccess()
        {
            Assert.AreEqual("1", converter.ToString(Pitches.C));
            Assert.AreEqual("1", converter.ToString(Pitches.Csharp));
            Assert.AreEqual("2", converter.ToString(Pitches.D));
            Assert.AreEqual("2", converter.ToString(Pitches.Dsharp));
            Assert.AreEqual("3", converter.ToString(Pitches.E));
            Assert.AreEqual("4", converter.ToString(Pitches.F));
            Assert.AreEqual("4", converter.ToString(Pitches.Fsharp));
            Assert.AreEqual("5", converter.ToString(Pitches.G));
            Assert.AreEqual("5", converter.ToString(Pitches.Gsharp));
            Assert.AreEqual("6", converter.ToString(Pitches.A));
            Assert.AreEqual("6", converter.ToString(Pitches.Asharp));
            Assert.AreEqual("7", converter.ToString(Pitches.B));
        }
    }
}
