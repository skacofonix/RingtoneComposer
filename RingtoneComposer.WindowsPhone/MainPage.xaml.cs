using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Audio;
using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;
using RingtoneComposer.Core.ViewModels;
using System;
using System.Windows;

namespace RingtoneComposer.WindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly ComposerViewModel model = new ComposerViewModel();
        private SoundEffectInstance player = null;
        private SaveRingtoneTask saveRingtoneChooser = null;

        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            saveRingtoneChooser = new SaveRingtoneTask();
            saveRingtoneChooser.Completed += new EventHandler<TaskEventArgs>(saveRingtoneChooser_Completed);

            model.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != "Partition")
                    return;

                model.Tune = Parse(model.Partition);
            };

            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            model.Partition = textBoxPartition.Text;
            model.Tune = Parse(model.Partition);
            PlayTune(model.Tune);
        }


        private Tune Parse(string partition)
        {
            var tuneConverter = new RttlConverter();
            return tuneConverter.Parse(partition);
        }

        private void PlayTune(Tune tune)
        {
            var oscillator = new SquareOscilator(44100);
            var soundGenerator = new SoundGenerator(oscillator);

            using (var s = soundGenerator.TuneToWaveStream(tune))
            {
                var effect = SoundEffect.FromStream(s);
                player = effect.CreateInstance();
                player.Play();
            }
        }

        private void Stop()
        {
            if (player != null && !player.IsDisposed && player.State == SoundState.Playing)
                player.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveRingtoneChooser_Completed(object sender, TaskEventArgs e)
        {
            switch (e.TaskResult)
            {
                //Logic for when the ringtone was saved successfully
                case TaskResult.OK:
                    MessageBox.Show("Ringtone saved.");
                    break;

                //Logic for when the task was cancelled by the user
                case TaskResult.Cancel:
                    MessageBox.Show("Save cancelled.");
                    break;

                //Logic for when the ringtone could not be saved
                case TaskResult.None:
                    MessageBox.Show("Ringtone could not be saved.");
                    break;
            }
        }

        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}