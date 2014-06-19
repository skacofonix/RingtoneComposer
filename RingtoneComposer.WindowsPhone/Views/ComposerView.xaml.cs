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
using System.Windows.Input;

namespace RingtoneComposer.WindowsPhone.Views
{
    public partial class ComposerView : MvxPhonePage
    {
        public ComposerView()
        {
            InitializeComponent();
        }

        private void textBoxPartition_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            char? c = ConvertKeyToChar(e);

            if (!c.HasValue)
                return;

            int position = textBoxPartition.SelectionStart;

            var viewModel = (ComposerViewModel)DataContext;
            if (viewModel.KeyPressedCommand.CanExecute(c.Value))
                viewModel.KeyPressedCommand.Execute(c.Value);
        }

        private static char? ConvertKeyToChar(System.Windows.Input.KeyEventArgs e)
        {
            char? c;

            switch (e.Key)
            {
                case Key.NumPad0:
                    c = '0';
                    break;
                case Key.NumPad1:
                    c = '1';
                    break;
                case Key.NumPad2:
                    c = '2';
                    break;
                case Key.NumPad3:
                    c = '3';
                    break;
                case Key.NumPad4:
                    c = '4';
                    break;
                case Key.NumPad5:
                    c = '5';
                    break;
                case Key.NumPad6:
                    c = '6';
                    break;
                case Key.NumPad7:
                    c = '7';
                    break;
                case Key.NumPad8:
                    c = '8';
                    break;
                case Key.NumPad9:
                    c = '9';
                    break;
                case Key.D8:
                    c = '*';
                    break;
                case Key.D3:
                    c = '#';
                    break;
                default:
                    c = null;
                    break;
            }
            return c;
        }
    }
}