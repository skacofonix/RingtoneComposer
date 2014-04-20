using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class NokiaKeypressConverterTest
    {
        private NokiaKeypressConverter converter = new NokiaKeypressConverter();

        [TestMethod]
        public void Parse_Success()
        {
            var partition = "(6)888#* (5)# 69999# 58888# 4# 4 (2)# (2)999 29# (6)8888# (5)# 69999# 488 4# 2 299# 288 4 5# 7 2* 49 (5)#** (4) 599 088888 88888";

            var tune = converter.Parse(partition);
            tune.Tempo = 100;
            tune.Name = "TocattaFugue";

            Assert.IsNotNull(tune);

            Assert.IsTrue(tune.TuneElementList != null && tune.TuneElementList.Count > 0);

            var rttlConverter = new RttlConverter();
            var rttl = rttlConverter.ToString(tune);
        }
    }
}
