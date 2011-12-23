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
using System.Data;

namespace Naari.Windows
{
    public partial class UpdateItem : Window
    {
        public delegate void ItemUpdatedHandler(bool updated);
        public event ItemUpdatedHandler ItemUpdated;
        int id;

        public UpdateItem()
        {
            InitializeComponent();
        }

        public UpdateItem(Item item)
        {
            InitializeComponent();
            PopulateVendors(item.Vendor);
            try
            {
                id = item.ID;
                this.DataContext = item;
                uiPurchaseDate.Value = (!string.IsNullOrEmpty(item.PurchaseDate)) ? Convert.ToDateTime(item.PurchaseDate) : (DateTime?)null;
                uiSellingDate.Value = (!string.IsNullOrEmpty(item.SellingDate)) ? Convert.ToDateTime(item.SellingDate) : (DateTime?)null;
            }
            catch (Exception)
            {
                MessageBox.Show("Error while initializing update");
            }
        }

        private void PopulateVendors(string vendor)
        {
            try
            {
                uiVendor.Items.Clear();
                string sql = "select Vendor from Naari_Vendor";
                DataTable dt = DataManager.GetData(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    uiVendor.Items.Add(dr["Vendor"].ToString());
                }
                uiVendor.Text = vendor;
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting vendors");
            }
        }

        private void uiOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBoxResult.Cancel == MessageBox.Show("Are you sure you want to update", "Update", MessageBoxButton.OKCancel))
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format(" update Naari set PurchaseDate = '{0}', Vendor = '{1}' ", uiPurchaseDate.Value, uiVendor.Text));
                sb.Append(string.Format(" , BillNumber = '{0}', ItemName = '{1}' ", uiBillNumber.Text, uiItemName.Text));
                sb.Append(string.Format(" , CostPrice = '{0}' ", uiCostPrice.Text));
                if (!string.IsNullOrEmpty(uiLocation.Text))
                {
                    sb.Append(string.Format(" , Location = '{0}' ", uiLocation.Text));
                }
                if (!string.IsNullOrEmpty(uiSellingPrice.Text))
                {
                    sb.Append(string.Format(" , SellingPrice = '{0}' ", uiSellingPrice.Text));
                }
                if (uiSellingDate.Value is DateTime)
                {
                    sb.Append(string.Format(" , SellingDate = '{0}' ", uiSellingDate.Value));
                }
                sb.Append(string.Format(" where ID = {0} ", id));

                DataManager.SetData(sb.ToString());
                if (ItemUpdated != null)
                {
                    ItemUpdated(true);
                }
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while updating");
            }
        }

        private void uiCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
