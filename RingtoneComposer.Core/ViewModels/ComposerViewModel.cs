using System.ComponentModel;

namespace RingtoneComposer.Core.ViewModels
{
    public class ComposerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Partition { get; set; }
        public Tune Tune { get; set; }
    }
}
