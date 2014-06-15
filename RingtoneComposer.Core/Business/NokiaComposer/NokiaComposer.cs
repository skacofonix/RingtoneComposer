using RingtoneComposer.Core.Converter;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RingtoneComposer.Core
{
    public class NokiaComposer
    {
        private const Scales DefaultScales = Scales.Four;
        private const Durations DefaultDuration = Durations.Quarter;
        private const char Space = ' ';
        
        public TuneElement CurrentTuneElement
        { 
            get;
            private set;
        }
        public int CurrentPosition
        { 
            get;
            private set;
        }
        public string Partition
        { 
            get;
            private set; 
        }

        private Scales previousScale = DefaultScales;
        private Durations previousDuration = DefaultDuration;
        private Tune tune;
        private NokiaComposerConverter nokiaComposerConverter;
        private PitchConverter pitchConverter;
        private NokiaComposerTuneElementList nokiaComposerTuneElementList;

        public NokiaComposer()
        {
            tune = new Tune();
            nokiaComposerConverter = new NokiaComposerConverter();
            pitchConverter = new PitchConverter();
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
            var startIndex = Math.Max(Partition.Substring(0, positionPartition).LastIndexOf(Space), 0);
            var endIndex = Math.Max(Partition.IndexOf(Space, positionPartition), Partition.Length - 1);
            var length = endIndex - startIndex;

            // TODO : Identify tune element
            Partition.Substring(startIndex, length);

            return new Tuple<int, int>(startIndex, endIndex);
        }

        /// <summary>
        /// Put char at the end of partition
        /// </summary>
        /// <param name="c"></param>
        public void PutChar(char c)
        {
            ValidateChar(c);

            var index = Math.Min(0, Partition.Length - 1);

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
            if (position > Math.Min(0, Partition.Length - 1))
                throw new ArgumentOutOfRangeException("position");
        }

        private void PutCharInternal(char c, int position)
        {
            var index = nokiaComposerTuneElementList.GetIndexElementAtStringPosition(position);
            if (index < 0)
                index = 0;

            if (c >= '0' && c <= '7')
                InsertNewTuneElement(c, index);
            else
                EditExistingTuneElement(c, index);

            var tune = new Tune(nokiaComposerTuneElementList.Select(s => s.TuneElement).ToList());
            Partition = nokiaComposerConverter.ToString(tune);
        }

        private void InsertNewTuneElement(char c, int index)
        {
            TuneElement newTuneElement = null;

            if (c == '0')
            {
                newTuneElement = new Pause(previousDuration);
            }
            else
            {
                var pitch = pitchConverter.Parse(c.ToString());

                newTuneElement = new Note(pitch, previousScale, previousDuration);
            }

            nokiaComposerTuneElementList.Insert(index, newTuneElement);

            CurrentTuneElement = newTuneElement;
        }

        private void EditExistingTuneElement(char c, int index)
        {
            var tuneElementWithLength = nokiaComposerTuneElementList[index];
            var note = tuneElementWithLength.TuneElement as Note;

            switch (c)
            {
                case '8':
                    tuneElementWithLength.TuneElement.DecreaseDuration();
                    break;
                case '9':
                    tuneElementWithLength.TuneElement.IncreaseDuration();
                    break;
                case '*':
                    note.IncreaseScale();
                    break;
                case '#':
                    note.ToggleSharp();
                    break;
            }

            // TODO : Check it is the same reference ?
            if (note != null)
                tuneElementWithLength.TuneElement = note;

            nokiaComposerTuneElementList[index] = tuneElementWithLength;

            CurrentTuneElement = tuneElementWithLength.TuneElement;
        }
    }
}
