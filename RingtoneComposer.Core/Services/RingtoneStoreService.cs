using System.Collections.Generic;

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
