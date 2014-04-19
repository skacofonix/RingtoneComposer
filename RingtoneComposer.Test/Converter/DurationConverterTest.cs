using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;
using System;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class DurationConverterTest
    {
        private DurationConverter converter = new DurationConverter();

        [TestMethod]
        public void ToString_Success()
        {
            Assert.AreEqual("1", converter.ToString(Durations.Whole));
            Assert.AreEqual("2", converter.ToString(Durations.Half));
            Assert.AreEqual("4", converter.ToString(Durations.Quarter));
            Assert.AreEqual("8", converter.ToString(Durations.Eight));
            Assert.AreEqual("16", converter.ToString(Durations.Sixteenth));
            Assert.AreEqual("32", converter.ToString(Durations.ThirtySecond));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Fail1()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_Fail2()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse(string.Empty));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_Fail3()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse(" "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_Fail4()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse("a"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_Fail5()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse("3"));
        }

        [TestMethod]
        public void Parse_Fail6()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse("2"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Parse_Fail()
        {
            Assert.AreNotEqual(Durations.Whole, converter.Parse(null));
            Assert.AreNotEqual(Durations.Whole, converter.Parse(string.Empty));
            Assert.AreNotEqual(Durations.Whole, converter.Parse(" "));
            Assert.AreNotEqual(Durations.Whole, converter.Parse("3"));
            Assert.AreNotEqual(Durations.Whole, converter.Parse("a"));
            Assert.AreNotEqual(Durations.Whole, converter.Parse("2"));
        }

        [TestMethod]
        public void Parse_Succes()
        {
            Assert.AreEqual(Durations.Whole, converter.Parse("1"));
            Assert.AreEqual(Durations.Half, converter.Parse("2"));
            Assert.AreEqual(Durations.Quarter, converter.Parse("4"));
            Assert.AreEqual(Durations.Eight, converter.Parse("8"));
            Assert.AreEqual(Durations.Sixteenth, converter.Parse("16"));
            Assert.AreEqual(Durations.ThirtySecond, converter.Parse("32"));
        }
    }
}
