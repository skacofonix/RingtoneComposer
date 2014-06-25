using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RingtoneComposer.Core.ViewModels
{
    public class ImporterViewModel : MvxViewModel
    {
        private string partitionImported;
        public string PartitionImported
        {
            get { return partitionImported; }
            set
            {
                partitionImported = value;
                RaisePropertyChanged(() => PartitionImported);
            }
        }

        private RttlConverter rttlConverter = new RttlConverter();
        private IRingtoneImporterService importerService;

        public ImporterViewModel(IRingtoneImporterService importerService)
        {
            this.importerService = importerService;
        }

        #region ImportCommand
        
        private MvxCommand importCommand;
        public ICommand ImportCommand
        {
            get
            { 
                if(importCommand == null)
                {
                    importCommand = new MvxCommand(() =>
                    {
                        var tune = importerService.Import(PartitionImported);
                        var rttl = rttlConverter.ToString(tune);
                        ShowViewModel<ComposerViewModel>(new ComposerParameters { Rttl = rttl });
                    },
                    () =>
                    {
                        return importerService.CheckPartitionValitity(PartitionImported);
                    });
                }
                return importCommand;
            }
        }

        #endregion
    }
}
