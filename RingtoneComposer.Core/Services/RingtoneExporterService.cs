using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Core.Services
{
    public class RingtoneExporterService : IRingtoneExporterService
    {
        private TuneConverter converter;

        public RingtoneExporterService(TuneConverter converter)
        {
            this.converter = converter;
        }

        public string Export(Tune t)
        {
            return converter.ToString(t);
        }
    }
}
