
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Business
{
    public class NokiaComposerTuneElementList : IList<TuneElementWithLength>
    {
        private List<TuneElementWithLength> list;

        public int Length
        {
            get
            {
                var elementsLength = list.Sum(t => t.Length);
                var nbSeparator = Math.Min(0, list.Count - 1);

                return elementsLength + nbSeparator;
            }
        }

        public int GetIndexElementAtStringLength(int position)
        {
            var totalLength = Length;
            if (position < 0 || position > totalLength)
                throw new ArgumentOutOfRangeException(string.Concat("index expected in range 0 to ", totalLength));

            int? startIndexFounded = null;

            int previousStartIndex = 0;
            int currentStartIndex = 0;
            int index = 0;
            do
            {
                if (currentStartIndex < position)
                {
                    startIndexFounded = previousStartIndex;
                }
                else
                {
                    previousStartIndex = currentStartIndex;

                    currentStartIndex += list[index].Length;
                    if (index == Math.Max(0, list.Count - 1))
                        currentStartIndex += 1;
                }

                index++;

            } while (index < list.Count || !startIndexFounded.HasValue);

            if (startIndexFounded.HasValue)
                return startIndexFounded.Value;
            return -1;
        }

        public int IndexOf(TuneElementWithLength item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TuneElement item)
        {
            list.Insert(index, new TuneElementWithLength(item));
        }

        public void Insert(int index, TuneElementWithLength item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public TuneElementWithLength this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(TuneElementWithLength item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(TuneElementWithLength item)
        {
            return list.Contains(item);
        }

        public void CopyTo(TuneElementWithLength[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TuneElementWithLength item)
        {
            return list.Remove(item);
        }

        public IEnumerator<TuneElementWithLength> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    public class TuneElementWithLength
    {
        public TuneElement TuneElement
        {
            get { return tuneElement; }
            set
            {
                tuneElement = value;
                Length = ComputeElementStringLength(tuneElement);
            }
        }
        private TuneElement tuneElement;

        public int Length
        {
            get;
            private set;
        }

        public TuneElementWithLength(TuneElement tuneElement)
        {
            TuneElement = tuneElement;
        }

        private int ComputeElementStringLength(TuneElement tuneElement)
        {
            int length = 1;

            if (tuneElement.Dotted)
                length += 1;

            if (tuneElement.Duration == (Durations.Sixteenth ^ Durations.ThirtySecond))
                length += 2;
            else
                length += 1;

            var note = tuneElement as Note;
            if (note != null)
            {
                length += 1;

                if (note.IsSharp)
                    length += 1;
            }

            return length;
        }
    }
}
