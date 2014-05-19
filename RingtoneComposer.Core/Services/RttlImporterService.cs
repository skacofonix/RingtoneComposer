using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public class RttlImporterService : IRingtoneImporterService
    {
        public bool CheckPartitionValitity(string s)
        {
            // TODO : Implement regex validation of RTTL partition
            return true;
        }
        public Tune Import(string s)
        {
            return converter.Parse(s);
        }

        private RttlConverter converter = new RttlConverter();
    }
}
