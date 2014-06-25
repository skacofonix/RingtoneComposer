using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Core.Services
{
    public class NokiaKeypressExporterService : IRingtoneExporterService
    {
        private NokiaKeypressConverter converter = new NokiaKeypressConverter();

        public string Export(Tune t)
        {
            return converter.ToString(t);
        }
    }
}
