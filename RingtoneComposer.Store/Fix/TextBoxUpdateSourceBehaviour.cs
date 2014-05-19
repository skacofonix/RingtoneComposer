using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RingtoneComposer.Store.Fix
{
    public class TextBoxUpdateSourceBehaviour
    {
        private static readonly Dictionary<TextBox, PropertyInfo> boundProperties = new Dictionary<TextBox, PropertyInfo>();

        public static readonly DependencyProperty BindingSourceProperty =
            DependencyProperty.RegisterAttached(
            "BindingSource",
            typeof(string),
            typeof(TextBoxUpdateSourceBehaviour),
            new PropertyMetadata(default(string), onBindingChanged));

        public static void SetBindingSource(TextBox element, string value)
        {
            element.SetValue(BindingSourceProperty, value);
        }

        public static string GetBindingSource(TextBox element)
        {
            return (string)element.GetValue(BindingSourceProperty);
        }

        private static void onBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var txtBox = d as TextBox;
            if (txtBox == null) return;
            txtBox.Loaded += onLoaded;
            txtBox.TextChanged += onTextChanged;
        }

        static void onLoaded(object sender, RoutedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if (txtBox == null) return;
            // Reflect the datacontext of the textbox to find the field to bind to.
            if (txtBox.DataContext == null) return;
            var dataContextType = txtBox.DataContext.GetType();
            addToBoundPropertyDictionary(txtBox, dataContextType.GetRuntimeProperty(GetBindingSource(txtBox)));
        }

        static void addToBoundPropertyDictionary(TextBox txtBox, PropertyInfo boundProperty)
        {
            PropertyInfo propInfo;
            if (!boundProperties.TryGetValue(txtBox, out propInfo))
            {
                boundProperties.Add(txtBox, boundProperty);
            }
        }

        static void onTextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if (txtBox != null)
            {
                boundProperties[txtBox].SetValue(txtBox.DataContext, txtBox.Text);
            }
        }
    }
}
