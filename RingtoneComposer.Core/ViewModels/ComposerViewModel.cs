using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Converter;
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
            private set
            {
                partition = value;
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

        private ISoundGeneratorService soundGeneratorService;
        private ISoundPlayer soundPlayer;
        private NokiaComposer nokiaComposer = new NokiaComposer();
        private RttlConverter rttlConverter = new RttlConverter();
        private NokiaComposerConverter nokiaComposerConverter = new NokiaComposerConverter();

        public ComposerViewModel(ISoundGeneratorService soundGeneratorService,
                                 ISoundPlayer soundPlayer)
        {
            this.soundGeneratorService = soundGeneratorService;
            this.soundPlayer = soundPlayer;
            Tune = new Tune { Tempo = 120 };
        }

        public void Init(ComposerParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Rttl))
            {
                Tune = rttlConverter.Parse(parameters.Rttl);
                Partition = nokiaComposerConverter.ToString(Tune);
            }
        }

        #region KeyPressesCommand

        private MvxCommand<char> keyPressedCommand;
        public ICommand KeyPressedCommand
        {
            get
            {
                if(keyPressedCommand == null)
                {
                    keyPressedCommand = new MvxCommand<char>(c =>
                    {
                        nokiaComposer.PutChar(c);
                        Partition = nokiaComposer.Partition;

                        var note = nokiaComposer.CurrentTuneElement as Note;
                        if (note != null)
                            soundPlayer.Play(note, tune.Tempo ?? 120);
                    },
                    (c) =>
                    {
                        return true;
                    });
                }
                return keyPressedCommand;
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
    }
}
