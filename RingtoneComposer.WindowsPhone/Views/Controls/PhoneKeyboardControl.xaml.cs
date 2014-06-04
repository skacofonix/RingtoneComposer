using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RingtoneComposer.WindowsPhone.Views.Controls
{
    public partial class PhoneKeyboardControl : UserControl
    {
        public PhoneKeyboardControl()
        {
            InitializeComponent();
        }



        #region DP KeyOne
        public Button KeyOne
        {
            get { return (Button)GetValue(KeyOneProperty); }
            set { SetValue(KeyOneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyOne.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyOneProperty =
            DependencyProperty.Register("KeyOne", typeof(Button), typeof(PhoneKeyboardControl), new PropertyMetadata("1"));
        #endregion

        #region DP KeyTwo
        public Button KeyTwo
        {
            get { return (Button)GetValue(KeyTwoProperty); }
            set { SetValue(KeyTwoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyTwo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyTwoProperty =
            DependencyProperty.Register("KeyTwo", typeof(Button), typeof(PhoneKeyboardControl), new PropertyMetadata("2"));
        #endregion

        


    }
}
