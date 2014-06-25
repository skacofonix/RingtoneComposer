using RingtoneComposer.Core.Converter;

namespace RingtoneComposer.Core.Services
{
    public class RttlImporterService : IRingtoneImporterService
    {
        private RttlConverter converter = new RttlConverter();

        public bool CheckPartitionValitity(string s)
        {
            return converter.CheckPartition(s);
        }
        public Tune Import(string s)
        {
            return converter.Parse(s);
        }
    }
}
