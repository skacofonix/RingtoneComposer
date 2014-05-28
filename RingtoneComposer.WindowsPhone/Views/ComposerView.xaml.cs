using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Cirrious.MvvmCross.WindowsPhone.Views;
using RingtoneComposer.Core.ViewModels;

namespace RingtoneComposer.WindowsPhone.Views
{
    public partial class ComposerView : MvxPhonePage
    {
        public ComposerView()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            var model = this.ViewModel as ComposerViewModel;
            if(model != null)
            {
                model.Partition = "TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p,";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = this.ViewModel as ComposerViewModel;
            model.Partition = "TocattaFugue:d=32,o=5,b=100:a#.,g#.,2a#,g#,f#,f,d#.,4d.,2d#,a#.,g#.,2a#,8f,8f#,8d,2d#,8d,8f,8g#,8b,8d6,4f6,4g#.,4f.,1g,32p,";
        }
    }
}