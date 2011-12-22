﻿using System;
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
    public partial class NewItem : Window
    {
        public delegate void NewItemCreatedHandler(bool created);
        public event NewItemCreatedHandler NewItemCreated;

        public NewItem()
        {
            InitializeComponent();
        }

        private void uiOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateFields())
                {
                    return;
                }
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
