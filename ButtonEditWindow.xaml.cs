using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace G_RPC
{
    public partial class ButtonEditWindow : Window
    {
        bool _button;
        public ButtonEditWindow(bool button, string buttonText, string buttonUrl)
        {
            InitializeComponent();
            _button = button;
            textbox_text.Text = buttonText;
            textbox_url.Text = buttonUrl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_button)
            {
                this.Title = "Button 2 edit";
            } else
            {
                this.Title = "Button 1 edit";
            }
            TextBlock_button.Text = this.Title;

            
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_text.Text != string.Empty)
            {
                Properties.Settings.Default.button1_text = textbox_text.Text;
            }
            if (textbox_url.Text != string.Empty)
            {
                Properties.Settings.Default.button1_url = textbox_url.Text;
            }
            DialogResult = true;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
