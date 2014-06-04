using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
