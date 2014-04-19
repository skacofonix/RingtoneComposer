using System;

namespace RingtoneComposer.Core
{
    public class SawToothOccilator : IOscillator, IDetuneable
    {
        private double radiansPerCircle = Math.PI * 2;
        private double currentFrequency = 2000;
        private double sampleRate = 44100;
        private double detune = 0.0;

        public SawToothOccilator(double sampleRate)
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
            double sinPart = Math.Sin(depthIntoOccilations * radiansPerCircle);
            sinPart += (depthIntoOccilations - 0.5);
            return sinPart;
        }

        public void SetDetune(double detune)
        {
            this.detune = detune;
        }
    }
}
