using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class TuneReadNokiaComposerSerializerTeste
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deserialize_Throw_ArgumentNullException()
        {
            var nokiaComposerConverter = new NokiaComposerConverter();

            Assert.IsNull(nokiaComposerConverter.Parse(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deserialize_throw_ArgumentException()
        {
            var nokiaComposerConverter = new NokiaComposerConverter();

            Assert.IsNull(nokiaComposerConverter.Parse(String.Empty));
            Assert.IsNull(nokiaComposerConverter.Parse(" "));
        }

        [TestMethod]
        public void Deserialize_Success()
        {
            const string partition = "8#f1 16#f1 16#f1 16#f1 8b1 32#d2 8b1 32#g1 8e1 8#c2 32a1 8#a1 32#c2 8#a1 32#f1 8#d1 8b1 32#f1 8b1 32#d 28b1 32#g1 8e1 8#c2 32b1 8#a1 32#f1 8#g1 32#a1 4b1 32#f1 8b1 32#d2 8b1 32#g1 8e1 8#c2 32a1 8#a1 32#c28#a1 32#f1 8#d1 8b1 32#f1 8b1 32#d2 8b1 32#g1 8e1";

            var nokiaComposerConverter = new NokiaComposerConverter();

            var t = nokiaComposerConverter.Parse(partition);

            Assert.IsNotNull(t);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serialize_Throw_ArgumentNullException()
        {
            var nokiaComposerConverter = new NokiaComposerConverter();

            nokiaComposerConverter.ToString(null);
        }

        [TestMethod]
        public void Serialize_Success()
        {
            var nokiaComposerConverter = new NokiaComposerConverter();

            var t = new Tune(new List<TuneElement>
              {  
                new Note(Pitchs.A, Scales.Four, Durations.Whole),
                new Note(Pitchs.Asharp, Scales.Five, Durations.Half),
                new Pause(Durations.Quarter)
              },
              100
            );

            Assert.AreEqual(nokiaComposerConverter.ToString(t), "A 2A#2 4- ");
        }
    }
}
