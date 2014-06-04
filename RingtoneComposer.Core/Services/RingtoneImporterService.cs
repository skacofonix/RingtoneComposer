using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public class RingtoneImporterService : IRingtoneImporterService
    {
        private TuneConverter converter;

        public RingtoneImporterService(TuneConverter converter)
        {
            this.converter = converter;
        }

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
