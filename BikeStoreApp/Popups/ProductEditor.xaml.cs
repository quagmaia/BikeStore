using Domain;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BikeStoreApp.Popups
{
    /// <summary>
    /// Interaction logic for ProductEditor.xaml
    /// </summary>
    public partial class ProductEditor : UserControl
    {
        /*
         *
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public decimal CommissionFactor { get; set; } 
         * */

        public ProductEditor(BikeStoreController controller)
        {
            InitializeComponent();
            ListView.ItemsSource = controller.Products.Values;
        }



    }
}
