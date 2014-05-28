using Microsoft.Xna.Framework.Audio;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.WindowsPhone
{
	public class SoundPlayer : ISoundPlayer
	{
		private SoundGenerator soundGenerator;
        private SoundEffectInstance player = null;
        
		public SoundPlayer ()
		{
            var oscillator = new SquareOscilator(44100);
		    soundGenerator = new SoundGenerator(oscillator);
		}

		public void Play(Tune tune)
		{
            using (var s = soundGenerator.TuneToWaveStream(tune))
            {
                var effect = SoundEffect.FromStream(s);
                player = effect.CreateInstance();
                player.Play();
            }
		}

		public void Stop()
		{
            if (player != null && !player.IsDisposed && player.State == SoundState.Playing)
                player.Stop();
		}
	}
}
