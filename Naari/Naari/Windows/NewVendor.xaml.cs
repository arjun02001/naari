using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Naari.Classes;

namespace Naari.Windows
{
    public partial class NewVendor : Window
    {
        public delegate void VendorAddedHandler(bool added);
        public event VendorAddedHandler VendorAdded;

        public NewVendor()
        {
            InitializeComponent();
        }

        private void uiAddVendor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(uiVendor.Text.Trim()))
                {
                    MessageBox.Show("Enter the new vendor");
                    return;
                }
                string sql = string.Format(" insert into Naari_Vendor (Vendor) values ('{0}') ", uiVendor.Text.Trim());
                DataManager.SetData(sql);
                if (VendorAdded != null)
                {
                    VendorAdded(true);
                }
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while adding new vendor");
            }
        }
    }
}
