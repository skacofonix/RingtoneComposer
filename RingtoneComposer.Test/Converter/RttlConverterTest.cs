using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core;
using System.Collections.Generic;

namespace RingtoneComposer.Test.Converter
{
    [TestClass]
    public class RttlConverterTest
    {
        private RttlConverter converter = new RttlConverter();

        [TestMethod]
        public void ToString_Success()
        {
            var tune = new Tune(new List<TuneElement>
              {  
                new Note(Pitches.A, Scales.Four, Durations.Whole),
                new Note(Pitches.Asharp, Scales.Five, Durations.Half),
                new Pause(Durations.Quarter)
              }
            );
            tune.Name = "TestName";

            Assert.AreEqual(converter.ToString(tune), "TestName:d=1,o=4,b=63:A,2A#5,4P,");
        }

        [TestMethod]
        public void Parse_Success()
        {
            var partition = "TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p";

            var tune = converter.Parse(partition);

            Assert.IsNotNull(tune);

            Assert.IsTrue(tune.TuneElementList != null && tune.TuneElementList.Count > 0);
        }
    }
}
