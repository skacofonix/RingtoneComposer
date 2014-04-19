using System;

namespace RingtoneComposer.Core
{
    public class SquareOscilator : IOscillator
    {
        private double radiansPerCircle = Math.PI * 2;
		private double currentFrequency = 2000;
		private double sampleRate = 44100;

        public SquareOscilator(double sampleRate)
        {
			this.sampleRate = sampleRate;
		}

		public void SetFrequency(double value) {
			currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			
			double samplesPerOccilation = (sampleRate / currentFrequency);
			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;

			double sinPart = Math.Sin(depthIntoOccilations * radiansPerCircle);
			if (depthIntoOccilations > 0.5) {
				sinPart += 0.5;
			}
			else {
				sinPart -= 0.5;
			}

			return sinPart;
		}
    }
}
