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
