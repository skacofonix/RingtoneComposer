using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RingtoneComposer.Core
{
    public class SoundGenerator
    {
        private const int SampleRate = 44100;
        private readonly IOscillator oscillator;

        public SoundGenerator(IOscillator oscillator)
        {
            this.oscillator = oscillator;
        }

        private IEnumerable<double> GenerateSoundData(Tune tune)
        {
            if (tune == null)
                throw new ArgumentNullException();

            var durationConverter = new DurationConverter();
            double period = 1 / (60d / tune.Tempo);

            foreach (var tuneElement in tune.TuneElementList)
            {
                var note = tuneElement as Note;
                if (note != null)
                {
                    var frequence = new Frequences()[note.Pitch, note.Scale];
                    oscillator.SetFrequency(frequence);
                }
                else if (tuneElement is Pause)
                {
                    oscillator.SetFrequency(0);
                }

                var loopLength = SampleRate * period * (durationConverter.GetValue(tuneElement.Duration) * (tuneElement.Dotted ? 1.5 : 1));
                for (var i = 0; i < loopLength; i++)
                    yield return oscillator.GetNext(i);
            }
        }

        public Stream TuneToWaveStream(Tune tune)
        {
            var soundData = GenerateSoundData(tune).ToList();

            const int RIFF = 0x46464952;
            const int WAVE = 0x45564157;
            const int formatChunkSize = 16;
            const int headerSize = 8;
            const int format = 0x20746D66;
            const short formatType = 1;

            const short tracks = 2;
            const short bitsPerSample = 16;
            const short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            const int bytesPerSecond = SampleRate * frameSize;
            const int waveSize = 4;
            const int data = 0x61746164;
            int samples = soundData.Count;
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write(RIFF);
            writer.Write(fileSize);
            writer.Write(WAVE);
            writer.Write(format);
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(SampleRate);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(data);
            writer.Write(dataChunkSize);

            double sample_l;
            short sl;
            foreach (var item in soundData)
            {
                sample_l = item * 30000.0;
                sample_l = Math.Max(sample_l, -32767.0f);
                sample_l = Math.Min(sample_l, 32767.0f);

                sl = (short)sample_l;
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
            }

            stream.Position = 0;

            return stream;
        }

        public Stream ToneToMp3Stream(Tune tune)
        {
            return null;
        }
    }
}
