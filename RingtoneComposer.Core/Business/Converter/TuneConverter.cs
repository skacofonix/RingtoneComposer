using System;
using System.Text.RegularExpressions;

namespace RingtoneComposer.Core.Converter
{
    public abstract class TuneConverter : IConverter<Tune>
    {
        protected readonly DurationConverter DurationConverter;
        protected readonly ScaleConverter ScaleConverter;
        protected readonly PitchConverter PitchConverter;

        protected const string Dot = ".";

        public abstract string PartitionValidatorPattern { get; }
        public abstract string TuneElementPattern { get; }
        public abstract string TuneElementDelimiter { get; }
        public abstract string Pause { get; }
        public abstract int DefaultBpm { get; }

        protected TuneConverter()
        {
            DurationConverter = new DurationConverter();
            ScaleConverter = new ScaleConverter();
            PitchConverter = new PitchConverter();
        }

        public bool CheckPartition(string s)
        {
            var regex = new Regex(PartitionValidatorPattern);
            return regex.IsMatch(s);
        }

        public string ToString(Tune t)
        {
            if (t == null)
                throw new ArgumentNullException("t");   

            return InternalToString(t);
        }

        protected abstract string InternalToString(Tune t);

        public Tune Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("s is empty");

            return InternalParse(s);
        }

        protected abstract Tune InternalParse(string s);
    }
}
