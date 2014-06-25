using System.Collections.Generic;

namespace RingtoneComposer.Core.Services
{
    public interface IRingtoneStoreService
    {
        IEnumerable<Tune> GetTunes();
    }
}
