using System.Collections.Generic;

namespace RingtoneComposer.Core
{
    public class Tune
    {
        public string Name { get; set; }

        public int Tempo { get; set; }

        public List<TuneElement> TuneElementList { get; private set; }

        public Tune()
        {
            Tempo = 120;
        }

        public Tune(List<TuneElement> tuneElementList)
            :this()
        {
            this.TuneElementList = tuneElementList;
        }

        public Tune(List<TuneElement> tuneElementList, int tempo)
            : this()
        {
            this.TuneElementList = tuneElementList;
            this.Tempo = tempo;
        }
    }
}
