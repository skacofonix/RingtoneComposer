using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core.Interfaces;
using RingtoneComposer.Core.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RingtoneComposer.Core.ViewModels
{
    public class RingtonesViewModel : MvxViewModel
    {
        private ObservableCollection<Tune> tunes;
        public ObservableCollection<Tune> MyTunes
        {
            get { return tunes; }
            private set
            {
                tunes = value;
                RaisePropertyChanged(() => MyTunes);
            }
        }

        private Tune selectedTune;
        private IRingtoneStoreService ringtoneStore;
        private ISoundPlayer soundPlayer;

        public RingtonesViewModel(
            IRingtoneStoreService ringtoneStore,
            ISoundPlayer soundPlayer)
        {
            this.ringtoneStore = ringtoneStore;
            this.soundPlayer = soundPlayer;

            //this.MyTunes = ringtoneStore.GetTunes().ToList();
        }

        public void Init()
        {
            LoadData();
        }

        private void LoadData()
        {
            var rttlConverter = new RttlConverter();

            var tunes = new ObservableCollection<Tune>
            {
                rttlConverter.Parse("TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p"),
                rttlConverter.Parse("TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p"),
                rttlConverter.Parse("TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p"),
                //rttlConverter.Parse("smwwd1:d=4,o=5,b=125:a,8f.,16c,16d,16f,16p,f,16d,16c,16p,16f,16p,16f,16p,8c6,8a.,g,16c,a,8f.,16c,16d,16f,16p,f,16d,16c,16p,16f,16p,16a#,16a,16g,2f,16p,8a.,8f.,8c,8a.,f,16g#,16f,16c,16p,8g#.,2g,8a.,8f.,8c,8a.,f,16g#,16f,8c,2c6"),
                //rttlConverter.Parse("smb:d=4,o=5,b=100:16e6,16e6,32p,8e6,16c6,8e6,8g6,8p,8g,8p,8c6,16p,8g,16p,8e,16p,8a,8b,16a#,8a,16g.,16e6,16g6,8a6,16f6,8g6,8e6,16c6,16d6,8b,16p,8c6,16p,8g,16p,8e,16p,8a,8b,16a#,8a,16g.,16e6,16g6,8a6,16f6,8g6,8e6,16c6,16d6,8b,8p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16g#,16a,16c6,16p,16a,16c6,16d6,8p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16c7,16p,16c7,16c7,p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16g#,16a,16c6,16p,16a,16c6,16d6,8p,16d#6,8p,16d6,8p,16c6"),
                //rttlConverter.Parse("smb_under:d=4,o=6,b=100:32c,32p,32c7,32p,32a5,32p,32a,32p,32a#5,32p,32a#,2p,32c,32p,32c7,32p,32a5,32p,32a,32p,32a#5,32p,32a#,2p,32f5,32p,32f,32p,32d5,32p,32d,32p,32d#5,32p,32d#,2p,32f5,32p,32f,32p,32d5,32p,32d,32p,32d#5,32p,32d#"),
                //rttlConverter.Parse("smbdeath:d=4,o=5,b=90:32c6,32c6,32c6,8p,16b,16f6,16p,16f6,16f.6,16e.6,16d6,16c6,16p,16e,16p,16c"),
                //rttlConverter.Parse("korobyeyniki:d=4,o=5,b=160:e6,8b,8c6,8d6,16e6,16d6,8c6,8b,a,8a,8c6,e6,8d6,8c6,b,8b,8c6,d6,e6,c6,a,2a,8p,d6,8f6,a6,8g6,8f6,e6,8e6,8c6,e6,8d6,8c6,b,8b,8c6,d6,e6,c6,a,a"),
                //rttlConverter.Parse("Tetris:d=4,o=5,b=160:d6,32p,c.6,32p,8a,8c6,8a#,16a,16g,f,c,8a,8c6,8g,8a,f,c,d,8d,8e,8g,8f,8e,8d,c,c,c"),
                //rttlConverter.Parse("tetris3:d=4,o=5,b=63:d#6,b,c#6,a#,16b,16g#,16a#,16b,16b,16g#,16a#,16b,c#6,g,d#6,16p,16g#,16a#,16b,c#6,16p,16b,16a#,g#,g,g#,16f,16g,16g#,16a#,8d#.6,32d#6,32p,32d#6,32p,32d#6,32p,16d6,16d#6,8f.6,16d6,8a#,8p,8f#6,8d#6,8f#,8g#,a#.,16p,16a#,8d#.6,16f6,16f#6,16f6,16d#6,16a#,8g#.,16b,8d#6,16f6,16d#6,8a#.,16b,16a#,16g#,16f,16f#,d#")
            };

            this.MyTunes = tunes;
        }

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
                        soundPlayer.Play(selectedTune);
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

        #region EditRingtoneCommand

        private MvxCommand editRingtoneCommand;
        public ICommand EditRingtoneCommand
        {
            get
            {
                if (editRingtoneCommand == null)
                {
                    editRingtoneCommand = new MvxCommand(() =>
                    {
                        ShowViewModel<ComposerViewModel>();
                    },
                    () =>
                    {
                        return true;
                    });
                }
                return editRingtoneCommand;
            }
        }

        #endregion

        #region ExportRingtoneCommand

        private MvxCommand exportRingtoneCommand;
        public ICommand ExportRingtoneCommand
        {
            get
            {
                if (exportRingtoneCommand == null)
                {
                    exportRingtoneCommand = new MvxCommand(() =>
                    {
                        ShowViewModel<ExporterViewModel>();
                    },
                    () =>
                    {
                        return true;
                    });
                }
                return exportRingtoneCommand;
            }
        }

        #endregion

        #region ImportRingtoneCommand

        private MvxCommand importRingtoneCommand;
        public ICommand ImportRingtoneCommand
        {
            get
            {
                if (importRingtoneCommand == null)
                {
                    importRingtoneCommand = new MvxCommand(() =>
                    {
                        ShowViewModel<ImporterViewModel>();

                    },
                    () =>
                    {
                        return true;
                    });
                }
                return importRingtoneCommand;
            }
        }

        #endregion
    }
}
