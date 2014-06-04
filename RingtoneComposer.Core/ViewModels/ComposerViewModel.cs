using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Interfaces;
using RingtoneComposer.Core.Services;
using System.IO;
using System.Windows.Input;

namespace RingtoneComposer.Core.ViewModels
{
    public class ComposerViewModel : MvxViewModel
    {
        private string partition;
        public string Partition
        {
            get { return partition; }
            set
            {
                partition = value;
                Tune = ringtoneImporterService.Import(partition);
                RaisePropertyChanged(() => Partition);
            }
        }

        private TuneElement currentTuneElement;
        public TuneElement CurrentTuneElement
        {
            get { return currentTuneElement; }
            private set
            { 
                currentTuneElement = value;
                RaisePropertyChanged(() => CurrentTuneElement);
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

        private Stream ringtoneMp3Stream;
        public Stream RingtoneMp3Stream
        {
            get { return ringtoneMp3Stream; }
            private set { ringtoneMp3Stream = value; }
        }

        private IRingtoneImporterService ringtoneImporterService;
        private ISoundGeneratorService soundGeneratorService;
        private ISoundPlayer soundPlayer;

        public ComposerViewModel(IRingtoneImporterService ringtoneImporterService,
                                 ISoundGeneratorService soundGeneratorService,
                                 ISoundPlayer soundPlayer)
        {
            this.ringtoneImporterService = ringtoneImporterService;
            this.soundGeneratorService = soundGeneratorService;
            this.soundPlayer = soundPlayer;
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

        #region PlayRingtoneCommand

        private MvxCommand playRingtoneCommand;
        public ICommand PlayRingtoneCommand
        {
            get
            {
                if (playRingtoneCommand == null)
                {
                    playRingtoneCommand = new MvxCommand(() =>
                    {
                        soundPlayer.Play(Tune);
                    },
                    () =>
                    {
                        return true;
                        //return Tune != null;
                    });
                }
                return playRingtoneCommand;
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


        private MvxCommand keyOnePressedCommand;
        public ICommand KeyOnePressedCommand
        {
            get
            {
                if(keyOnePressedCommand == null)
                {
                    keyOnePressedCommand = new MvxCommand(() =>
                    {
                        // TODO
                    },
                    () =>
                    {
                        return true;
                    });
                }
                return keyOnePressedCommand;
            }
        }
    }
}
