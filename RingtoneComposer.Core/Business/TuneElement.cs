using RingtoneComposer.Core.Helpers;
using System;
namespace RingtoneComposer.Core
{
    public abstract class TuneElement
    {
        public Durations Duration { get; set; }

        public bool Dotted { get; set; }  

        protected TuneElement(Durations duration = Durations.Half, bool dotted = false)
        {
            Duration = duration;
            Dotted = dotted;
        }

        public bool IncreaseDuration()
        {
            var newDuration = DurationHelper.Increase(Duration);

            bool isSuccess = newDuration != Duration;

            if (isSuccess)
                Duration = newDuration;

            return isSuccess;
        }

        public bool DecreaseDuration()
        {
            var newDuration = DurationHelper.Decrease(Duration);

            bool isSuccess = newDuration != Duration;

            if (isSuccess)
                Duration = newDuration;

            return isSuccess;
        }
    }
}
