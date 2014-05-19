using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public interface IRingtoneExporterService
    {
        string Export(Tune t);
    }
}
