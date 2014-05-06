using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using System.Linq;

namespace RingtoneComposer.Test
{
    [TestClass]
    public class NoteTest
    {
        private Note note = new Note(Pitches.D, Scales.Five, Durations.Quarter);

        [TestMethod]
        public void IncreasePitch_Success()
        {
            Assert.IsTrue(note.IncreasePitch());
            Assert.AreEqual(Pitches.Dsharp, note.Pitch);
        }

        [TestMethod]
        public void DecreasePitch_Success()
        {
            Assert.IsTrue(note.DecreasePitch());
            Assert.AreEqual(Pitches.Csharp, note.Pitch);
        }

        private Pitches[] pitchArray = new Pitches[] { 
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

        private Scales[] scaleArray = new Scales[] {
                Scales.Four,
                Scales.Five,
                Scales.Six,
                Scales.Seven
            };

        [TestMethod]
        public void IncreasePitcheRange_Success()
        {
            var note = new Note(Pitches.C, Scales.Four, Durations.Quarter);

            foreach (var scale in scaleArray)
            {
                foreach (var pitch in pitchArray)
                {
                    var expectedNote = new Note(pitch, scale, Durations.Quarter);

                    Assert.AreEqual(expectedNote.Pitch, note.Pitch);
                    Assert.AreEqual(expectedNote.Scale, note.Scale);

                    note.IncreasePitch();
                }
            }
        }

        [TestMethod]
        public void DecreasePitcheRange_Success()
        {
            var note = new Note(Pitches.B, Scales.Seven, Durations.Quarter);

            foreach (var scale in scaleArray.Reverse())
            {
                foreach (var pitch in pitchArray.Reverse())
                {
                    var expectedNote = new Note(pitch, scale, Durations.Quarter);

                    Assert.AreEqual(expectedNote.Pitch, note.Pitch);
                    Assert.AreEqual(expectedNote.Scale, note.Scale);

                    note.DecreasePitch();
                }
            }
        }

        [TestMethod]
        public void IncreaseScale_Success()
        {
            Assert.IsTrue(note.IncreaseScale());
            Assert.AreEqual(Scales.Six, note.Scale);
        }

        [TestMethod]
        public void DecreaseScale_Success()
        {
            Assert.IsTrue(note.DecreaseScale());
            Assert.AreEqual(Scales.Four, note.Scale);
        }

        [TestMethod]
        public void IncreaseDuration_Success()
        {
            Assert.IsTrue(note.IncreaseDuration());
            Assert.AreEqual(Durations.Half, note.Duration);
        }

        [TestMethod]
        public void DecreaseDuration_Success()
        {
            Assert.IsTrue(note.DecreaseDuration());
            Assert.AreEqual(Durations.Eight, note.Duration);
        }
    }
}
