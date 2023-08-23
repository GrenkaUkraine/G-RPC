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
    /// <summary>
    /// Interaction logic for IfTrayDialog.xaml
    /// </summary>
    public partial class IfTrayDialog : Window
    {
        public IfTrayDialog()
        {
            InitializeComponent();
        }

        private void button_no_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingsCheckBox();
            DialogResult = false;
        }

        private void button_yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            SaveSettingsCheckBox();
        }

        private void SaveSettingsCheckBox()
        {
            Properties.Settings.Default.tray_closing_q = (bool)checkbox_q.IsChecked;
            Properties.Settings.Default.Save();
        }
    }
}
