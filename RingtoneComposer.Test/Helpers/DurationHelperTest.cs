using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Helpers;

namespace RingtoneComposer.Test.Helpers
{
    [TestClass]
    public class DurationHelperTest
    {
        [TestMethod]
        public void IncreaseDuration_Success()
        {
            var duration = Durations.ThirtySecond;

            duration = duration.Increase();
            Assert.AreEqual(Durations.Sixteenth, duration);

            duration = duration.Increase();
            Assert.AreEqual(Durations.Eight, duration);

            duration = duration.Increase();
            Assert.AreEqual(Durations.Quarter, duration);

            duration = duration.Increase();
            Assert.AreEqual(Durations.Half, duration);

            duration = duration.Increase();
            Assert.AreEqual(Durations.Whole, duration);

            duration = duration.Increase();
            Assert.AreEqual(Durations.Whole, duration);
        }

        [TestMethod]
        public void DecreaseDuration_Success()
        {
            var duration = Durations.Whole;

            duration = duration.Decrease();
            Assert.AreEqual(Durations.Half, duration);

            duration = duration.Decrease();
            Assert.AreEqual(Durations.Quarter, duration);

            duration = duration.Decrease();
            Assert.AreEqual(Durations.Eight, duration);

            duration = duration.Decrease();
            Assert.AreEqual(Durations.Sixteenth, duration);

            duration = duration.Decrease();
            Assert.AreEqual(Durations.ThirtySecond, duration);

            duration = duration.Decrease();
            Assert.AreEqual(Durations.ThirtySecond, duration);
        }
    }
}