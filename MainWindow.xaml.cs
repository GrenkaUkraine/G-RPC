using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace G_RPC
{
    public partial class MainWindow : Window
    {
        private NotifyIcon _tray;
        private DiscordRPC discordRPC;
        private bool buttonConnect = true;
        #region Window
        public MainWindow()
        {
            InitializeComponent();
            InitializeTray();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Properties.Settings.Default.tray_closing_q)
            {
                IfTrayDialog ifTrayDialog = new IfTrayDialog();
                bool? dialogResult = ifTrayDialog.ShowDialog();

                if (dialogResult == true)
                {
                    Properties.Settings.Default.tray_closing = true;
                }
                else if (dialogResult == false)
                {
                    Properties.Settings.Default.tray_closing = false;
                }
            }
            else if (Properties.Settings.Default.tray_closing)
            {
                e.Cancel = true;
                this.Hide();
                _tray.Visible = true;
            }
            SaveSettings();
        }
        #endregion
        #region Tray
        private void InitializeTray()
        {
            _tray = new NotifyIcon();
            _tray.Icon = Properties.Resources.G_RPC_icon_ico;
            _tray.Visible = false;

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem openItem = new ToolStripMenuItem("Open");
            openItem.Click += openItem_Click;
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += exitItem_Click;

            _tray.DoubleClick += _tray_DoubleClick;

            contextMenuStrip.Items.Add(openItem);
            contextMenuStrip.Items.Add(exitItem);

            _tray.ContextMenuStrip = contextMenuStrip;
        }

        private void _tray_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            _tray.Visible = false;
        }

        private void openItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("openItem_Click");
            this.Show();
            _tray.Visible = false;
        }
        private void exitItem_Click(object sender, EventArgs e)
        {
            _tray.Dispose();
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
        #region Settings
        private void LoadSettings()
        {
            var settings = Properties.Settings.Default;
            InitializeButton();
            ChangeButtonName(false, settings.button1_text);
            ChangeButtonName(true, settings.button2_text);
            textblock_appid.Text = NoneString(settings.app_id);
            textblock_large_name.Text = NoneString(settings.img_large_name);
            textblock_large_text.Text = NoneString(settings.img_large_text);
            textblock_small_name.Text = NoneString(settings.img_small_name);
            textblock_large_text.Text = NoneString(settings.img_small_text);
            textblock_state.Text = NoneString(settings.state);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.app_id = NoneString(textblock_appid.Text, true);
            Properties.Settings.Default.img_large_name = NoneString(textblock_large_name.Text, true);
            Properties.Settings.Default.img_large_text = NoneString(textblock_large_text.Text, true);
            Properties.Settings.Default.img_small_name = NoneString(textblock_small_name.Text, true);
            Properties.Settings.Default.img_small_text = NoneString(textblock_small_text.Text, true);
            Properties.Settings.Default.state = NoneString(textblock_state.Text, true);

            Properties.Settings.Default.Save();
        }
        #endregion
        #region Strings
        private string NoneString(string text, bool invert = false)
        {
            if (invert)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    return "none";
                }
                else
                {
                    return text;
                }
            }
            else
            {
                if (text == "none")
                {
                    return string.Empty;
                }
                else
                {
                    return text;
                }
            }
        }
        #endregion
        #region Buttons
        private void button_add_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.buttons_count < 2)
            {
                Properties.Settings.Default.buttons_count++;
                AddButton();
            }
        }

        private void button_remove_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.buttons_count > 0)
            {
                Properties.Settings.Default.buttons_count--;
                RemoveButton();
            }
        }

        private void AddButton()
        {
            if (Properties.Settings.Default.buttons_count == 1)
            {
                button_1.Visibility = Visibility.Visible;
                button_ui_1.Visibility = Visibility.Visible;
            }
            else if (Properties.Settings.Default.buttons_count == 2)
            {
                button_2.Visibility = Visibility.Visible;
                button_ui_2.Visibility = Visibility.Visible;
            }
        }

        private void RemoveButton()
        {
            if (Properties.Settings.Default.buttons_count == 1)
            {
                button_2.Visibility = Visibility.Collapsed;
                button_ui_2.Visibility = Visibility.Collapsed;
            }
            else if (Properties.Settings.Default.buttons_count == 0)
            {
                button_1.Visibility = Visibility.Collapsed;
                button_ui_1.Visibility = Visibility.Collapsed;
            }
        }

        private void InitializeButton()
        {
            if (Properties.Settings.Default.buttons_count == 1)
            {
                button_1.Visibility = Visibility.Visible;
                button_ui_1.Visibility = Visibility.Visible;
            }
            else if (Properties.Settings.Default.buttons_count == 2)
            {
                button_1.Visibility = Visibility.Visible;
                button_ui_1.Visibility = Visibility.Visible;
                button_2.Visibility = Visibility.Visible;
                button_ui_2.Visibility = Visibility.Visible;
            }
        }
        private void ChangeButtonName(bool button, string name)
        {
            if (button)
            {
                button_2.Content = name;
                button_ui_2.Content = name;
            }
            else
            {
                button_1.Content = name;
                button_ui_1.Content = name;
            }
        }

        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            ButtonEditWindow buttonEditWindow = new ButtonEditWindow(false, Properties.Settings.Default.button1_text, Properties.Settings.Default.button1_url);
            bool result = buttonEditWindow.ShowDialog() ?? false;

            if (result)
            {
                ChangeButtonName(false, Properties.Settings.Default.button1_text);
            }
        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            ButtonEditWindow buttonEditWindow = new ButtonEditWindow(true, Properties.Settings.Default.button2_text, Properties.Settings.Default.button2_url);
            bool result = buttonEditWindow.ShowDialog() ?? false;

            if (result)
            {
                ChangeButtonName(true, Properties.Settings.Default.button1_text);
            }
        }
        #endregion

        private void textblock_state_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textblock_state.Text))
            {
                textbox_state.Text = textblock_state.Text;
            }
            else
            {
                textbox_state.Text = "State";
            }
        }

        private void textblock_details_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textblock_details.Text))
            {
                textbox_details.Text = textblock_details.Text;
            }
            else
            {
                textbox_details.Text = "Details";
            }
        }

        private void textblock_appid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttonConnect)
            {
                if (textblock_appid.Text.Length > 0)
                {
                    button_connect.Background = (Brush)System.Windows.Application.Current.Resources["button_green"];
                }
                else
                {
                    button_connect.Background = (Brush)System.Windows.Application.Current.Resources["button_red"];
                }
            }
        }
        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            if (buttonConnect)
            {
                if (textblock_appid.Text.Length > 0)
                {
                    discordRPC = new DiscordRPC();
                    discordRPC.StartRPC(
                        textblock_appid.Text,
                        textblock_details.Text,
                        textblock_state.Text,
                        textblock_large_name.Text,
                        textblock_large_text.Text,
                        textblock_small_name.Text,
                        textblock_small_text.Text,
                        Properties.Settings.Default.buttons_count,
                        NoneString(Properties.Settings.Default.button1_text),
                        NoneString(Properties.Settings.Default.button1_url),
                        NoneString(Properties.Settings.Default.button2_text),
                        NoneString(Properties.Settings.Default.button2_url)
                    );
                    button_connect.Content = "Close connection";
                    button_connect.Background = (Brush)System.Windows.Application.Current.Resources["button_default"];
                    buttonConnect = !buttonConnect;
                }
            }
            else
            {
                discordRPC.CloseConnection();
                discordRPC = null;
                if (textblock_appid.Text.Length > 0)
                {
                    button_connect.Background = (Brush)System.Windows.Application.Current.Resources["button_green"];
                }
                else
                {
                    button_connect.Background = (Brush)System.Windows.Application.Current.Resources["button_red"];
                }
                button_connect.Content = "Connect";
                buttonConnect = !buttonConnect;
            }
        }

        private void textblock_large_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textblock_large_name.Text.Length > 0)
            {
                image_Large.Visibility = Visibility.Visible;
            }
            else
            {
                image_Large.Visibility = Visibility.Collapsed;
            }
        }

        private void textblock_small_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textblock_small_name.Text.Length > 0)
            {
                image_Small.Visibility = Visibility.Visible;
            }
            else
            {
                image_Small.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
            Properties.Settings.Default.Save();
        }
    }
}