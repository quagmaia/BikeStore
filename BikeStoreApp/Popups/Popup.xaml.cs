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
        }

        public void ImplementConfiguration()
        {
            ConfirmBtn.Content = Config.ConfirmText;
            ConfirmBtn.Visibility = Config.ConfirmVisibility;

            BackBtn.Content = Config.BackText;
            BackBtn.Visibility = Config.BackVisibility;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Config.ConfirmCallback != null)
            {
                Config.ConfirmCallback.Invoke(sender, e);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Config.BackCallback != null)
            {
                Config.BackCallback.Invoke(sender, e);
            }
        }
    }

    public class PopupConfig
    {
        public string ConfirmText { get; set; } = "Confirm";
        public string BackText { get; set; } = "Cancel";
        public Visibility ConfirmVisibility { get; set; } = Visibility.Hidden;
        public Visibility BackVisibility { get; set; } = Visibility.Visible;
        public Action<object, RoutedEventArgs> ConfirmCallback { get; set; }
        public Action<object, RoutedEventArgs> BackCallback { get; set; }
    }
}
