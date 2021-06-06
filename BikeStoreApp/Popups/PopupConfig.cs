using System;
using System.Windows;
using System.Windows.Controls;

namespace BikeStoreApp.Popups
{
    public class PopupConfig
    {
        public string ConfirmText { get; set; } = "Confirm";
        public string BackText { get; set; } = "Cancel";
        public Visibility ConfirmVisibility { get; set; } = Visibility.Hidden;
        public Action<object, RoutedEventArgs> ConfirmCallback { get; set; }
        public Control Content { get; set; }
    }
}
