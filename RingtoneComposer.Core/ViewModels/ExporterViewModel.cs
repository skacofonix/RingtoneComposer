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

        private RttlConverter rttlConverter = new RttlConverter();
        private IRingtoneExporterService exporterService;

        private List<IRingtoneExporterService> exporterServices;

        public ExporterViewModel()
        {
            // TODO : Resolve list of IGingtoneExporterService
            //this.exporterService = Mvx.Resolve<IRingtoneExporterService>();
        }

        public void Init(ExporterParameters parameters)
        {
            if(parameters != null && !string.IsNullOrEmpty(parameters.Rttl))
            {
                tune = rttlConverter.Parse(parameters.Rttl);
                PartitionExported = exporterService.Export(tune);
            }
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
