using System;

namespace RingtoneComposer.Core
{
    class SawToothOscilatorSteadyDetunable : IOscillator, IDetuneable
    {
        private double radiansPerCircle = Math.PI * 2;
        private double currentFrequency = 2000;
        private double sampleRate = 44100;
        private double maxDetune = 0.0;
        private double currentDetune = 0.0;
        private bool detuneHigh = true;
        private double chanceOfChanging = 0.1;
        private double maximumChangeInDetunePerChange = 0.05;

        Random detuneRandom = new Random();
        public SawToothOscilatorSteadyDetunable(double sampleRate)
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

            if (maxDetune != 0.0)
            {

                if ((1 / chanceOfChanging) == (1 / detuneRandom.NextDouble()))
                {
                    if (detuneHigh)
                    {
                        currentDetune += (maxDetune * maximumChangeInDetunePerChange);
                    }
                    else
                    {
                        currentDetune -= (maxDetune * maximumChangeInDetunePerChange);
                    }
                    if (currentDetune >= maxDetune || currentDetune <= (maxDetune * -1))
                    {
                        detuneHigh = !detuneHigh;
                    }
                }

                samplesPerOccilation = samplesPerOccilation * ((1 - (maxDetune / 2)) + (currentDetune));
            }

            double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
            double sinPart = Math.Sin(depthIntoOccilations * radiansPerCircle);
            sinPart += (depthIntoOccilations - 0.5);
            return sinPart;
        }

        public void SetDetune(double maxDetune)
        {
            this.maxDetune = maxDetune;
        }
    }
}
