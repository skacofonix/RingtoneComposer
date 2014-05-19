using System;

namespace RingtoneComposer.Core
{
    public class SawToothOscilatorDetunable : IOscillator, IDetuneable
    {
        private double radiansPerCircle = Math.PI * 2;
        private double currentFrequency = 2000;
        private double sampleRate = 44100;
        private double detune = 0.0;
        Random detuneRandom = new Random();

        public SawToothOscilatorDetunable(double sampleRate)
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

            if (detune != 0.0)
            {
                samplesPerOccilation = samplesPerOccilation * (1 + (detuneRandom.NextDouble() * detune));
            }

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
