using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Core.Services
{
    public class RttlExporterService : IRingtoneExporterService
    {
        private RttlConverter converter = new RttlConverter();

        public string Export(Tune t)
        {
            return converter.ToString(t);
        }
    }
}
