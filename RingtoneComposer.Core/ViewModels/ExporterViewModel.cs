using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core.Services;
using RingtoneComposer.Core.ViewModels.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RingtoneComposer.Core.ViewModels
{
    public class ExporterViewModel : MvxViewModel
    {
        private string partitionExported;
        public string PartitionExported
        {
            get { return partitionExported; }
            private set
            {
                partitionExported = value;
                RaisePropertyChanged(() => PartitionExported);
            }
        }

        private TuneConverter converter;
        public TuneConverter Converter
        {
            get { return converter; }
            set
            { 
                converter = value;
                exporterService = new RingtoneExporterService(converter);
                RaisePropertyChanged(() => Converter);
            }
        }

        private Tune tune;

        private IRingtoneExporterService exporterService;

        public ExporterViewModel(Tune tune)
        {
            this.tune = tune;
            this.exporterService = Mvx.Resolve<IRingtoneExporterService>();
        }

        public ExporterViewModel(Tune tune, IRingtoneExporterService exporterService)
        {
            this.tune = tune;
            this.exporterService = exporterService;
        }

        public void Init(ExporterParameters parameters)
        {

        }

        #region ExportCommand

        private MvxCommand exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new MvxCommand(() =>
                    {
                        PartitionExported = exporterService.Export(tune);
                    },
                    () =>
                    {
                        return tune != null;
                    });
                }
                return exportCommand;
            }
        }

        #endregion
    }
}
