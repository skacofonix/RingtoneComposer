using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using System.Collections.Generic;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class FrequencesTest
    {
        private Frequences frequenceRepository = new Frequences();

        [TestMethod]
        public void GetFrequence_Success()
        {
            Pitches[] pitchArray = new Pitches[] { 
                Pitches.C,
                Pitches.Csharp,
                Pitches.D,
                Pitches.Dsharp,
                Pitches.E,
                Pitches.F,
                Pitches.Fsharp,
                Pitches.G,
                Pitches.Gsharp,
                Pitches.A,
                Pitches.Asharp,
                Pitches.B
            };

            Scales[] scaleArray = new Scales[] {
                Scales.Four,
                Scales.Five,
                Scales.Six,
                Scales.Seven
            };

            List<double> frequences = new List<double>();
            double? previousFrequence = null;
            foreach (var scale in scaleArray)
            {
                foreach (var pitch in pitchArray)
                {
                    var frequence = frequenceRepository[pitch, scale];
                    Assert.IsNotNull(frequence);

                    if(previousFrequence.HasValue)
                        Assert.IsTrue(previousFrequence < frequence);

                    previousFrequence = frequence;

                    Assert.IsFalse(frequences.Contains(frequence));

                    frequences.Add(frequence);
                }
            }

            Assert.AreEqual(scaleArray.Length * pitchArray.Length, frequences.Count);
        }
    }
}
