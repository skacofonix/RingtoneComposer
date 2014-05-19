using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Services;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace RingtoneComposer.Core.ViewModels
{
    public class ComposerViewModel : MvxViewModel
    {
        private string partition = "TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p,";
        public string Partition
        {
            get { return partition; }
            set
            {
                partition = value;
                RaisePropertyChanged(() => Partition);
            }
        }

        private Tune tune;
        public Tune Tune
        {
            get { return tune; }
            private set
            {
                tune = value;
                RaisePropertyChanged(() => Tune);
            }
        }

        private Stream ringtoneWaveStream;
        public Stream RingtoneWaveStream
        {
            get { return ringtoneWaveStream; }
            private set { ringtoneWaveStream = value; }
        }

        private Stream ringtoneMp3Stream;
        public Stream RingtoneMp3Stream
        {
            get { return ringtoneMp3Stream; }
            private set { ringtoneMp3Stream = value; }
        }

        private IRingtoneImporterService ringtoneImporterService;
        private ISoundGeneratorService soundGeneratorService;

        public ComposerViewModel(IRingtoneImporterService ringtoneImporterService, ISoundGeneratorService soundGeneratorService)
        {
            this.ringtoneImporterService = ringtoneImporterService;
            this.soundGeneratorService = soundGeneratorService;
        }

        #region ParsePartitionCommand

        private MvxCommand parsePartitionCommand;
        public ICommand ParsePartitionCommand
        {
            get
            {
                if (parsePartitionCommand == null)
                {
                    parsePartitionCommand = new MvxCommand(() =>
                    {
                        Tune = ringtoneImporterService.Import(partition);
                    },
                    () =>
                    {
                        return true;
                        //return ringtoneImporterService.CheckPartitionValitity(partition);
                    });
                }
                return parsePartitionCommand;
            }
        }

        #endregion

        #region GenerateWaveCommand

        private MvxCommand generateWaveCommand;
        public ICommand GenerateWaveCommand
        {
            get
            {
                if (generateWaveCommand == null)
                {
                    generateWaveCommand = new MvxCommand(() =>
                    {
                        ringtoneWaveStream = soundGeneratorService.TuneToWaveStream(tune);
                    },
                    () =>
                    {
                        return tune != null;
                    });
                }
                return generateWaveCommand;
            }
        }

        #endregion

        #region SaveRingtoneCommand

        private MvxCommand saveRingtoneCommand;
        public ICommand SaveRingtoneCommand
        {
            get
            {
                if (saveRingtoneCommand == null)
                {
                    saveRingtoneCommand = new MvxCommand(() =>
                    {
                        ringtoneMp3Stream = soundGeneratorService.TuneToMp3Stream(tune);
                    },
                    () =>
                    {
                        return tune != null;
                    });
                }
                return saveRingtoneCommand;
            }
        }

        #endregion
    }
}
