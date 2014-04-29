using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core;
using System.Collections.Generic;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class NokiaKeypressConverterTest
    {
        private NokiaKeypressConverter converter = new NokiaKeypressConverter();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Fail1()
        {
            converter.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pars_Fail2()
        {
            converter.Parse(string.Empty);
        }

        [TestMethod]
        public void Parse_Success()
        {
            var partition = "(6)888# (5)# 69999# 58888# 4# 4 (2)# (2)999 29# (6)8888# (5)# 69999# 488 4# 2 299# 288 4 5# 7 2* 49 (5)#** (4) 599 088888 88888";

            var tune = converter.Parse(partition);
            tune.Tempo = 100;
            tune.Name = "TocattaFugue";

            Assert.IsNotNull(tune);

            Assert.IsTrue(tune.TuneElementList != null && tune.TuneElementList.Count > 0);

            var rttlConverter = new RttlConverter();
            var rttl = rttlConverter.ToString(tune);
        }

        [TestMethod]
        public void ToString_Success()
        {
            var tune = new Tune(new List<TuneElement>
              {  
                new Note(Pitches.A, Scales.Four, Durations.Whole),
                new Note(Pitches.Asharp, Scales.Five, Durations.Half),
                new Pause(Durations.Quarter)
              },
             100
           );

            var expectedString = "6999 68#* 08";

            Assert.AreEqual(converter.ToString(tune), expectedString);
        }
    }
}
