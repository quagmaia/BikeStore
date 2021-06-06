using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BikeStoreApp.Popups
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        public PopupConfig Config { get; }
        public Popup(PopupConfig config)
        {
            Config = config;
            InitializeComponent(); 
            ImplementConfiguration();
        }

        public void ImplementConfiguration()
        {
            ConfirmBtn.Content = Config.ConfirmText;
            ConfirmBtn.Visibility = Config.ConfirmVisibility;

            BackBtn.Content = Config.BackText;

            Grid.SetColumn(Config.Content, 0);
            Grid.SetColumnSpan(Config.Content, 2);
            Grid.SetRow(Config.Content, 0);
            Config.Content.Focus();
            Config.Content.Visibility = Visibility.Visible;
            PopupGrid.Children.Add(Config.Content);
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Config.ConfirmCallback != null)
            {
                try
                {
                    Config.ConfirmCallback.Invoke(sender, e);
                    Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex}", "Update failed!", MessageBoxButton.OK);
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
