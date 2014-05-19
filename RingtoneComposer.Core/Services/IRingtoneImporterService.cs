using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public interface IRingtoneImporterService
    {
        bool CheckPartitionValitity(string s);

        Tune Import(string s);
    }
}
