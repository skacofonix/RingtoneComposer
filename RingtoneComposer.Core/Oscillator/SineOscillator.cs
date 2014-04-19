using System;

namespace RingtoneComposer.Core
{
    public class SineOscillator : IOscillator
    {
        private double radiansPerCircle = Math.PI * 2;
        private double currentFrequency = 2000;
        private double sampleRate = 44100;

        public SineOscillator(double sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        public void SetFrequency(double value)
        {
            currentFrequency = value;
        }

        public double GetNext(int sampleNumberInSecond)
        {
            double samplesPerOccilation = (sampleRate / currentFrequency);
            double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
            return Math.Sin(depthIntoOccilations * radiansPerCircle);
        }
    }
}
