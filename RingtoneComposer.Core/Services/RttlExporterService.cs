using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
