using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public Tuple<int, int> SelectTuneElement(int position)
        {
            //if (position > (partition.Length - 1))
            //    return;

            // Move to last char of tune element
            var positionPartition = position;
            while (positionPartition == Space && position > 0)
                positionPartition--;

            // Find start & end of tune element
            var startIndex = Math.Max(partition.Substring(0, positionPartition).LastIndexOf(Space), 0);
            var endIndex = Math.Max(partition.IndexOf(Space, positionPartition), partition.Length - 1);

            // TODO : Identify tune element
            partition.Substring(0, positionPartition);

            return new Tuple<int, int>(startIndex, endIndex);
        }

        public void PutChar(char c)
        {
            
        }

        public void PutChar(NokiaTouch t)
        {
            switch (t)
            {
                case NokiaTouch.One:
                case NokiaTouch.Two:
                case NokiaTouch.Three:
                case NokiaTouch.Four:
                case NokiaTouch.Five:
                case NokiaTouch.Six:
                case NokiaTouch.Seven:
                    currentElement = new Note();
                    break;
                case NokiaTouch.Zero:
                    break;
                case NokiaTouch.Up:
                case NokiaTouch.Down:
                    break;
                case NokiaTouch.Star:
                case NokiaTouch.Sharp:
                    break;
                case NokiaTouch.Eight:
                case NokiaTouch.Nine:
                    break;
                default :
                    throw new InvalidOperationException();
            }
        }
    }

    public enum NokiaTouch
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Zero,
        Star,
        Sharp,
        Up,
        Down
    }

    public abstract class NokiaComposerElement
    {
        public void Add(NokiaTouch t)
        {
            if(IsValid(t))
                AddInternal(t);
        }

        protected abstract bool IsValid(NokiaTouch t);

        protected abstract void AddInternal(NokiaTouch t);
    }

    public class NewNote : NokiaComposerElement
    {
        protected override bool IsValid(NokiaTouch t)
        {
            switch (t)
            {
                case NokiaTouch.One:
                case NokiaTouch.Two:
                case NokiaTouch.Three:
                case NokiaTouch.Four:
                case NokiaTouch.Five:
                case NokiaTouch.Six:
                case NokiaTouch.Seven:
                    return true;
                default :
                    return false;
            }
        }

        protected override void AddInternal(NokiaTouch t)
        {
            throw new NotImplementedException();
        }
    }


}
