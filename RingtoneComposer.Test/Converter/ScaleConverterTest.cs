using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;
using System;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class ScaleConverterTest
    {
        private ScaleConverter converter = new ScaleConverter();

        [TestMethod]
        public void ToString_Success()
        {
            Assert.AreEqual("4", converter.ToString(Scales.Four));
            Assert.AreEqual("5", converter.ToString(Scales.Five));
            Assert.AreEqual("6", converter.ToString(Scales.Six));
            Assert.AreEqual("7", converter.ToString(Scales.Seven));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Fail1()
        {
            converter.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_Fail2()
        {
            converter.Parse(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_Fail3()
        {
            converter.Parse(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_Fail4()
        {
            converter.Parse("A");
        }

        [TestMethod]
        public void Parse_Success()
        {
            Assert.AreEqual(Scales.Four, converter.Parse("4"));
            Assert.AreEqual(Scales.Five, converter.Parse("5"));
            Assert.AreEqual(Scales.Six, converter.Parse("6"));
            Assert.AreEqual(Scales.Seven, converter.Parse("7"));
        }
    }
}
