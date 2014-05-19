namespace RingtoneComposer.Core
{
    public interface IOscillator
    {
        double GetNext(int sampleNumberInSecond);
        void SetFrequency(double value);
    }
}
