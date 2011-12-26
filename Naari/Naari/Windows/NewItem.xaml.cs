//Arjun Mukherji
using System;
using System.Text;
using System.Windows;
using Naari.Classes;
using System.Data;

namespace Naari.Windows
{
    public partial class NewItem : Window
    {
        public delegate void NewItemCreatedHandler(bool created);
        public event NewItemCreatedHandler NewItemCreated;

        public NewItem()
        {
            InitializeComponent();
            PopulateVendors();
        }

        private void PopulateVendors()
        {
            try
            {
                uiVendor.Items.Clear();
                string sql = " select Vendor from Naari_Vendor ";
                DataTable dt = DataManager.GetData(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    uiVendor.Items.Add(dr["Vendor"].ToString());
                }
                uiVendor.SelectedIndex = 0;
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
                if (!ValidateFields())
                {
                    return;
                }
                for (int i = Convert.ToInt32(uiMultiple.Text); i > 0; i--)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" insert into Naari (PurchaseDate, Vendor, BillNumber, ItemName, CostPrice ");
                    if (!string.IsNullOrEmpty(uiLocation.Text))
                    {
                        sb.Append(" , Location ");
                    }
                    if (!string.IsNullOrEmpty(uiSellingPrice.Text))
                    {
                        sb.Append(" , SellingPrice ");
                    }
                    if (null != uiSellingDate.Value)
                    {
                        sb.Append(" , SellingDate ");
                    }
                    sb.Append(" ) ");
                    sb.Append(string.Format(" values ('{0}', '{1}', '{2}', '{3}', '{4}' ", uiPurchaseDate.Value, uiVendor.Text, uiBillNumber.Text, uiItemName.Text, uiCostPrice.Text));
                    if (!string.IsNullOrEmpty(uiLocation.Text))
                    {
                        sb.Append(string.Format(" , '{0}' ", uiLocation.Text));
                    }
                    if (!string.IsNullOrEmpty(uiSellingPrice.Text))
                    {
                        sb.Append(string.Format(" , '{0}' ", uiSellingPrice.Text));
                    }
                    if (null != uiSellingDate.Value)
                    {
                        sb.Append(string.Format(" , '{0}' ", uiSellingDate.Value));
                    }
                    sb.Append(" ) ");
                    DataManager.SetData(sb.ToString());
                }
                if (NewItemCreated != null)
                {
                    NewItemCreated(true);
                }
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while creating new item");
            }
        }

        private void uiCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            try
            {
                if (null == uiPurchaseDate.Value)
                {
                    MessageBox.Show("Enter purchase date"); return false;
                }
                if (string.IsNullOrEmpty(uiVendor.Text))
                {
                    MessageBox.Show("Enter vendor"); return false;
                }
                if (string.IsNullOrEmpty(uiBillNumber.Text))
                {
                    MessageBox.Show("Enter Bill number"); return false;
                }
                if (string.IsNullOrEmpty(uiItemName.Text))
                {
                    MessageBox.Show("Enter Item name"); return false;
                }
                if (string.IsNullOrEmpty(uiCostPrice.Text))
                {
                    MessageBox.Show("Enter cost price"); return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while validating fields");
            }
            return true;
        }
    }
}
