using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class RttlConverterTest
    {
        private RttlConverter converter = new RttlConverter();

        [TestMethod]
        public void ToString_Success()
        {
            Assert.IsTrue(false, "Not yet implemented");
        }

        [TestMethod]
        public void Parse_Success()
        {
            var partition = "";

            var tune = converter.Parse(partition);

            Assert.IsNotNull(tune);

            Assert.IsTrue(tune.TuneElementList != null && tune.TuneElementList.Count > 0);
        }
    }
}
