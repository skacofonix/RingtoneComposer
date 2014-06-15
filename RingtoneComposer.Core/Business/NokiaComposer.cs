using RingtoneComposer.Core.Business;
using RingtoneComposer.Core.Converter;
using System;
using System.Text.RegularExpressions;

namespace RingtoneComposer.Core
{
    public class NokiaComposer
    {
        private int currentPosition = 0;
        private TuneElement currentElement = null;
        private Tune tune = null;
        private Scales previousScale;
        private Durations previousDuration;
        private NokiaComposerConverter nokiaComposerConverter = null;
        private PitchConverter pitchConverter = new PitchConverter();
        private string partition = string.Empty;
        private const char Space = ' ';
        private NokiaComposerTuneElementList nokiaComposerTuneElementList;

        public NokiaComposer()
        {
            tune = new Tune();
            nokiaComposerConverter = new NokiaComposerConverter();
            nokiaComposerTuneElementList = new NokiaComposerTuneElementList();
        }

        public Tuple<int, int> SelectTuneElement(int position)
        {
            ValidatePosition(position);

            return GetTuneElementPositionRange(position);
        }

        private Tuple<int, int> GetTuneElementPositionRange(int index)
        {
            // Move to last char of tune element
            var positionPartition = index;
            while (positionPartition == Space && index > 0)
                positionPartition--;

            // Find start & end of tune element
            var startIndex = Math.Max(partition.Substring(0, positionPartition).LastIndexOf(Space), 0);
            var endIndex = Math.Max(partition.IndexOf(Space, positionPartition), partition.Length - 1);
            var length = endIndex - startIndex;

            // TODO : Identify tune element
            partition.Substring(startIndex, length);

            return new Tuple<int, int>(startIndex, endIndex);
        }

        /// <summary>
        /// Put char at the end of partition
        /// </summary>
        /// <param name="c"></param>
        public void PutChar(char c)
        {
            ValidateChar(c);

            var index = Math.Min(0, partition.Length - 1);

            PutCharInternal(c, index);
        }

        /// <summary>
        /// Put char at index position
        /// </summary>
        /// <param name="c"></param>
        /// <param name="position"></param>
        public void PutChar(char c, int position)
        {
            ValidateChar(c);
            ValidatePosition(position);

            PutCharInternal(c, position);
        }

        private void ValidateChar(char c)
        {
            var keyRegex = new Regex("[0-9*#]");
            if (!keyRegex.IsMatch(c.ToString()))
                throw new ArgumentOutOfRangeException("c parameter expected in range 0 to 9 or * or #");
        }

        private void ValidatePosition(int position)
        {
            if (position > Math.Min(0, partition.Length - 1))
                throw new ArgumentOutOfRangeException("position");
        }

        private void PutCharInternal(char c, int position)
        {
            var index = nokiaComposerTuneElementList.GetIndexElementAtStringPosition(position);
            if (index < 0)
                index = 0;

            if (c >= '0' && c <= '7')
            {
                TuneElement newTuneElement = null;

                if(c == '0')
                {
                    newTuneElement = new Pause(previousDuration);
                }
                else
                {
                    var pitch = pitchConverter.Parse(c.ToString());

                    newTuneElement = new Note(pitch, previousScale, previousDuration);
                }

                nokiaComposerTuneElementList.Insert(index, newTuneElement);
            }
            else
            {
                // Modify current tune element
                // it is necessarly to identify this
                
                var tuneElementWithLength = nokiaComposerTuneElementList[index];
                var note = tuneElementWithLength.TuneElement as Note;

                switch(c)
                {
                    case '8' :
                        tuneElementWithLength.TuneElement.DecreaseDuration();
                        break;
                    case '9':
                        tuneElementWithLength.TuneElement.IncreaseDuration();
                        break;
                    case '*' :
                        note.IncreaseScale();
                        break;
                    case '#' :
                        note.ToggleSharp();
                        break;
                }

                // TODO : Check it is the same reference ?
                if (note != null)
                    tuneElementWithLength.TuneElement = note;

                nokiaComposerTuneElementList[index] = tuneElementWithLength;
            }

        }
    }
}
