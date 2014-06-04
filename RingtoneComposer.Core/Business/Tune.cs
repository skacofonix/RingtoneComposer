using System.Collections.Generic;

namespace RingtoneComposer.Core
{
    public class Tune
    {
        public string Name { get; set; }

        public int? Tempo { get; set; }

        public List<TuneElement> TuneElementList { get; private set; }

        public Tune()
        {
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

        public override string ToString()
        {
            return string.Format("Name:{0}, Tempo:{1}", Name, Tempo.HasValue ? Tempo.Value.ToString() : "Null");
        }
    }
}
