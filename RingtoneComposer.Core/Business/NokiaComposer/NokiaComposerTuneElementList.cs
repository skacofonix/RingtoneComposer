
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core
{
    public class NokiaComposerTuneElementList : IList<NokiaComposerTuneElementWithLength>
    {
        private List<NokiaComposerTuneElementWithLength> list = new List<NokiaComposerTuneElementWithLength>();

        public int Length
        {
            get
            {
                var elementsLength = list.Sum(t => t.Length);
                var nbSeparator = Math.Max(0, list.Count - 1);

                return elementsLength + nbSeparator;
            }
        }

        public int GetIndexElementAtStringPosition(int position)
        {
            ValidatePosition(position);

            int? startPosition;
            int? index;
            FindTuneElementAtStringPosition(position, out startPosition, out index);

            if (index.HasValue)
                return index.Value;
            return -1;
        }

        public int GetStartPositionAtStringPosition(int position)
        {
            ValidatePosition(position);

            int? startPosition;
            int? index;
            FindTuneElementAtStringPosition(position, out startPosition, out index);

            if (startPosition.HasValue)
                return startPosition.Value;
            return -1;
        }

        public Tuple<int, int> GetRangePositionAtStringPosition(int position)
        {
            ValidatePosition(position);

            int? startPosition;
            int? index;
            FindTuneElementAtStringPosition(position, out startPosition, out index);

            if (!startPosition.HasValue || !index.HasValue)
                return null;

            var tuneElement = list[index.Value];
            var endPosition = startPosition + tuneElement.Length;

            var result = new Tuple<int, int>(startPosition.Value, endPosition.Value);

            return result;
        }

        private void ValidatePosition(int position)
        {
            var totalLength = Length;
            if (position < 0 || position > totalLength)
                throw new ArgumentOutOfRangeException(string.Concat("index expected in range 0 to ", totalLength));
        }

        private void FindTuneElementAtStringPosition(int position, out int? startPositionFounded, out int? indexFounded)
        {
            startPositionFounded = null;
            indexFounded = 0;

            if (!list.Any())
                return;

            int previousStartIndex = 0;
            int currentStartIndex = 0;
            do
            {
                if (currentStartIndex >= position)
                {
                    startPositionFounded = previousStartIndex;
                }
                else
                {
                    previousStartIndex = currentStartIndex;

                    if (indexFounded < list.Count)
                    {
                        currentStartIndex += list[indexFounded.Value].Length;
                        if (indexFounded == Math.Max(0, list.Count - 1))
                            currentStartIndex += 1;

                        indexFounded++;
                    }
                }
            } while (indexFounded < list.Count && !startPositionFounded.HasValue);
        }

        public int IndexOf(NokiaComposerTuneElementWithLength item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TuneElement item)
        {
            list.Insert(index, new NokiaComposerTuneElementWithLength(item));
        }

        public void Insert(int index, NokiaComposerTuneElementWithLength item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public NokiaComposerTuneElementWithLength this[int index]
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

        public void Add(NokiaComposerTuneElementWithLength item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(NokiaComposerTuneElementWithLength item)
        {
            return list.Contains(item);
        }

        public void CopyTo(NokiaComposerTuneElementWithLength[] array, int arrayIndex)
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

        public bool Remove(NokiaComposerTuneElementWithLength item)
        {
            return list.Remove(item);
        }

        public IEnumerator<NokiaComposerTuneElementWithLength> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
