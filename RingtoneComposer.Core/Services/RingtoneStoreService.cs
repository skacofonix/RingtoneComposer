using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public class RingtoneStoreService : IRingtoneStoreService
    {
        public IEnumerable<Tune> GetTunes()
        {
            return new List<Tune>();
        }
    }
}
