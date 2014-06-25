
namespace RingtoneComposer.Core.Services
{
    public interface IRingtoneImporterService
    {
        bool CheckPartitionValitity(string s);

        Tune Import(string s);
    }
}
