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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BikeCommon;
using BikeStoreApp.Popups;
using Domain;

namespace BikeStoreApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BikeStoreController Controller;
        public MainWindow()
        {
            InitializeComponent();
            Controller = new BikeStoreController();
            UpdateDirectory();
        }

        private void UpdateDirectory()
        {
            try
            {
                DirectoryLbl.Content = Controller.GetWorkingDirectory();
                DirectoryLbl.Background = UiConstants.NonErrorBackground;
            }
            catch (BikeException e)
            {
                DirectoryLbl.Background = UiConstants.ErrorBackground;
                DirectoryLbl.Content = e.Message;
            }
        }

        private void ChangeDirectoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var updateDirectoryTextBox = new TextBox()
            {
                Text = Controller.TryGetWorkingDirectory(),
                IsEnabled = true,
                BorderThickness = new Thickness(1,1,1,1),
                BorderBrush = Brushes.Black,
                Height = 20,
                Width = 300,
                Visibility = Visibility.Visible
            };

            Action<object, RoutedEventArgs> updatedDirCallback = delegate (object s, RoutedEventArgs rea) 
            {
                var newText = updateDirectoryTextBox.Text;
                Controller.SetWorkingDirectory(newText);
                Controller.Reload();
                UpdateDirectory();
            };

            var popupConfig = new PopupConfig()
            {
                Content = updateDirectoryTextBox,
                ConfirmCallback = updatedDirCallback,
                ConfirmVisibility = Visibility.Visible
            };

            var popup = new Popup(popupConfig);
            popup.Show();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.Save();
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Save failed!\n {ex}", "Dangit!", MessageBoxButton.OK);
            }
        }


    }
}
