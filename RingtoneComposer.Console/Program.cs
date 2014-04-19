using System.IO;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Serializer;

namespace RingtoneComposer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //DoGenerateaveWithSquareOscillator();

            DoParseRttlString();
        }

        private static void DoGenerateaveWithSquareOscillator()
        {
            const int sampleRate = 44100;
            const string partition =
                "16#d1 32f1 32f1 16f1 8f1 16#d1 16#d1 16#d1 16#d1 32#f1 32#f1 16#f1 8#f1 16f1 16f1 16f1 16#d1 32f1 32f1 16f1 8f1 16#d1 16#d1 16#d1 16#d1 32#f1 32#f1 16#f1 8#f1 16f1 16e1 16#d1 16#d2 2d2 16#a1 16#g1 2#a1";
            const int tempo = 20;

            var fullpath = Path.ChangeExtension(Path.GetTempFileName(), "wav");

            var oscillator = new SawToothOccilator(sampleRate);
            var soundGenerator = new SoundGenerator(oscillator);

            var tuneReader = new TuneReader();
            var tune = tuneReader.Read(partition, tempo);
            soundGenerator.SaveToWaveFile(fullpath, tune);

            System.Console.WriteLine(fullpath);

            System.Console.ReadLine();

            //var data = new List<double>();
            //tune.TuneElementList.Cast<pitch>().Select(s => s.NoteFrequence.Frequence).ToList().ForEach(e =>
            //{
            //    oscillator.SetFrequency(e);
            //    for (int i = 0; i < sampleRate; i++)
            //        data.Add(oscillator.GetNext(i));
            //});

            //SoundGenerator.SaveIntoStream(data.ToArray(), data.Count(), sampleRate);
        }

        private static void DoParseRttlString()
        {
            const int sampleRate = 44100;
            const string rttlString = "TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p,";
            var deserializer = new RttlSerializer();
            var tune = deserializer.Deserialize(rttlString);

            var fullpath = Path.ChangeExtension(Path.GetTempFileName(), "wav");

            var oscillator = new SawToothOccilator(sampleRate);
            var soundGenerator = new SoundGenerator(oscillator);

            tune.Tempo = 25;

            soundGenerator.SaveToWaveFile(fullpath, tune);

            System.Console.WriteLine(fullpath);

            System.Console.ReadLine();
        }
    }
}
