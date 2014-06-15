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
        private string partition = string.Empty;
        private const char Space = ' ';

        public NokiaComposer()
        {
            tune = new Tune();
            nokiaComposerConverter = new NokiaComposerConverter();
        }

        public Tuple<int, int> SelectTuneElement(int index)
        {
            ValidateIndex(index);

            return GetTuneElementPositionRange(index);
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
        /// <param name="index"></param>
        public void PutChar(char c, int index)
        {
            ValidateChar(c);
            ValidateIndex(index);

            PutCharInternal(c, index);
        }

        private void ValidateChar(char c)
        {
            var keyRegex = new Regex("[0-9*#]");
            if (!keyRegex.IsMatch(c.ToString()))
                throw new ArgumentOutOfRangeException("c parameter expected in range 0 to 9 or * or #");
        }

        private void ValidateIndex(int index)
        {
            if (index > Math.Min(0, partition.Length - 1))
                throw new ArgumentOutOfRangeException("index");
        }

        private void PutCharInternal(char c, int index)
        {
            if (c >= '0' && c <= '7')
            {
                // New tune element
            }
            else
            {
                // Modify current tune element
                // it is necessarly to identify this
                var blop = SelectTuneElement(index);
            }

        }

    }
}
