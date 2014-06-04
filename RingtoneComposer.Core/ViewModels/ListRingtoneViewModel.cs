using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Interfaces;
using RingtoneComposer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RingtoneComposer.Core.ViewModels
{
    public class ListRingtoneViewModel : MvxViewModel
    {
        private IList<Tune> tunes;
        public IList<Tune> Tunes
        {
            get { return tunes; }
            private set
            {
                tunes = value;
                RaisePropertyChanged(() => Tunes);
            }
        }

        private Tune selectedTune;

        private IRingtoneStoreService ringtoneStore;
        private ISoundPlayer soundPlayer;

        public ListRingtoneViewModel(
            IRingtoneStoreService ringtoneStore,
            ISoundPlayer soundPlayer)
        {
            this.Tunes = ringtoneStore.Tunes.ToList();
            this.soundPlayer = soundPlayer;
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
                        // TODO : Navigate to ComposerView
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
                        // TODO : Navigate to ExportView
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
                        // TODO : Navigate to ImportView
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
