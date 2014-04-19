using System;

namespace RingtoneComposer.Core
{
    class RoyalSawToothOscilator : IOscillator
    {
        private double radiansPerCircle = Math.PI * 2;
        private double currentFrequency = 2000;
        private double sampleRate = 44100;

        private double lastValue = 0.01;
        private double maximumRateOfChange = 0.5;

        public RoyalSawToothOscilator(double sampleRate)
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

            if ((sinPart - lastValue) > maximumRateOfChange)
            {
                sinPart = maximumRateOfChange;
            }
            if ((sinPart - lastValue) < (maximumRateOfChange * -1))
            {
                sinPart = (maximumRateOfChange * -1);
            }

            return sinPart;
        }
    }
}
