using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Helpers;
using System.Linq;

namespace RingtoneComposer.Test.Helpers
{
    [TestClass]
    public class NoteHelperTest
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

        [TestMethod]
        public void NoteIncrease_Success()
        {
            var note = new Note(Pitches.C, Scales.Four, Durations.Quarter);

            foreach (var scale in scaleArray)
            {
                foreach (var pitch in pitchArray)
                {
                    var expectedNote = new Note(pitch, scale, Durations.Quarter);

                    Assert.AreEqual(expectedNote.Pitch, note.Pitch);
                    Assert.AreEqual(expectedNote.Scale, note.Scale);

                    note.Increase();
                }
            }
        }

        [TestMethod]
        public void NoteDecrease_Success()
        {
            var note = new Note(Pitches.B, Scales.Seven, Durations.Quarter);

            foreach (var scale in scaleArray.Reverse())
            {
                foreach (var pitch in pitchArray.Reverse())
                {
                    var expectedNote = new Note(pitch, scale, Durations.Quarter);

                    Assert.AreEqual(expectedNote.Pitch, note.Pitch);
                    Assert.AreEqual(expectedNote.Scale, note.Scale);

                    note.Decrease();
                }
            }
        }
    }
}
