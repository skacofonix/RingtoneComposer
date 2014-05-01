using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Helpers;

namespace RingtoneComposer.Test.Helpers
{
    [TestClass]
    public class ScaleHelperTest
    {
        [TestMethod]
        public void IncreaseScale_Success()
        {
            var scale = Scales.Four;

            scale = scale.Increase();
            Assert.AreEqual(Scales.Five, scale);

            scale = scale.Increase();
            Assert.AreEqual(Scales.Six, scale);

            scale = scale.Increase();
            Assert.AreEqual(Scales.Seven, scale);

            scale = scale.Increase();
            Assert.AreEqual(Scales.Seven, scale);
        }

        [TestMethod]
        public void IncreaseScaleModulo_Success()
        {
            var scale = Scales.Four;

            for (int i = 0; i < 1; i++)
            {
                Assert.AreEqual(Scales.Four, scale);

                scale = scale.Increase();
                Assert.AreEqual(Scales.Five, scale);

                scale = scale.Increase();
                Assert.AreEqual(Scales.Six, scale);

                scale = scale.Increase();
                Assert.AreEqual(Scales.Seven, scale);
            }
        }

        [TestMethod]
        public void DecreaseScale_Success()
        {
            var scale = Scales.Seven;

            scale = scale.Decrease();
            Assert.AreEqual(Scales.Six, scale);

            scale = scale.Decrease();
            Assert.AreEqual(Scales.Five, scale);

            scale = scale.Decrease();
            Assert.AreEqual(Scales.Four, scale);

            scale = scale.Decrease();
            Assert.AreEqual(Scales.Four, scale);
        }
    }
}
