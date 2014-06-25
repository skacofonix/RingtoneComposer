using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class NokiaComposerConverterTest
    {
        private NokiaComposerConverter converter = new NokiaComposerConverter();

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
            converter.Parse(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_Fail3()
        {
            converter.Parse(" ");
        }

        [TestMethod]
        public void Parse_Success()
        {
            const string partition = "8#f1 16#f1 16#f1 16#f1 8b1 32#d2 8b1 32#g1 8e1 8#c2 32a1 8#a1 32#c2 8#a1 32#f1 8#d1 8b1 32#f1 8b1 32#d 28b1 32#g1 8e1 8#c2 32b1 8#a1 32#f1 8#g1 32#a1 4b1 32#f1 8b1 32#d2 8b1 32#g1 8e1 8#c2 32a1 8#a1 32#c28#a1 32#f1 8#d1 8b1 32#f1 8b1 32#d2 8b1 32#g1 8e1";

            var tune = converter.Parse(partition);

            Assert.IsNotNull(tune);

            Assert.IsTrue(tune.TuneElementList != null && tune.TuneElementList.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_Fail1()
        {
            converter.ToString(null);
        }

        [TestMethod]
        public void ToString_Success()
        {
            var t = new Tune(new List<TuneElement>
              {  
                new Note(Pitches.A, Scales.Four, Durations.Whole),
                new Note(Pitches.Asharp, Scales.Five, Durations.Half),
                new Pause(Durations.Quarter)
              },
              100
            );

            Assert.AreEqual(converter.ToString(t).Trim(), "1A1 2A#2 4-");
        }
    }
}
