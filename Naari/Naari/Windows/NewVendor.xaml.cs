//Arjun Mukherji
using System;
using System.Windows;
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
