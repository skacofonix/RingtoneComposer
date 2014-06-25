
namespace RingtoneComposer.Core.Interfaces
{
    public interface ISoundPlayer
    {
        void Play(Tune tune);

        void Play(Note note, int bpm);

        void Stop();
    }
}
