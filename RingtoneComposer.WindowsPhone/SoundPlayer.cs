using Microsoft.Xna.Framework.Audio;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Interfaces;

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
			Stop();

			using (var s = soundGenerator.TuneToWaveStream(tune))
			{
				var effect = SoundEffect.FromStream(s);
				player = effect.CreateInstance();
				player.Play();
			}
		}

		public void Play(Note note, int bpm)
		{
			Stop();

			using (var s = soundGenerator.NoteToWaveStream(note, bpm))
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
