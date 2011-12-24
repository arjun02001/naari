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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Naari.Classes;
using Naari.Windows;
using System.Data;

namespace Naari
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateItems();
            PopulateVendorFilter();
        }

        private void PopulateItems()
        {
            uiDataGrid.ItemsSource = Item.GetAllItems();
            uiTotalItems.Text = string.Format("Total Items = {0}", uiDataGrid.Items.Count);
        }

        private void PopulateVendorFilter()
        {
            uiVendorFilter.Items.Clear();
            uiVendorFilter.Items.Add("Vendor Filter");
            string sql = "select Vendor from Naari_Vendor";
            DataTable dt = DataManager.GetData(sql);
            foreach (DataRow dr in dt.Rows)
            {
                uiVendorFilter.Items.Add(dr["Vendor"].ToString());
            }
            uiVendorFilter.Items.Add("Add New Vendor");
            uiVendorFilter.SelectedIndex = 0;
        }

        private void uiAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            NewItem newItem = new NewItem();
            newItem.NewItemCreated += (c) =>
                {
                    if (c)
                    {
                        PopulateItems();
                    }
                };
            newItem.Show();
        }

        private void uiUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if (null == uiDataGrid.SelectedItem)
            {
                MessageBox.Show("Select item to update");
                return;
            }
            UpdateItem updateItem = new UpdateItem((Item)uiDataGrid.SelectedItem);
            updateItem.ItemUpdated += (u) =>
                {
                    if (u)
                    {
                        PopulateItems();
                    }
                };
            updateItem.Show();
        }

        private void uiDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (null == uiDataGrid.SelectedItem)
            {
                MessageBox.Show("Select item to delete");
                return;
            }
            if(MessageBoxResult.Cancel == MessageBox.Show("Are you sure you want to delete this", "Delete", MessageBoxButton.OKCancel))
            {
                return;
            }
            string sql = string.Format(" delete from Naari where ID = {0} ", (((Item)uiDataGrid.SelectedItem).ID));
            DataManager.SetData(sql);
            PopulateItems();
        }

        private void uiShowAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (uiShowAll.SelectedIndex)
                {
                    case 0: uiDataGrid.ItemsSource = Item.GetAllItems(); break;
                    case 1: uiDataGrid.ItemsSource = Item.GetSoldItems(); break;
                    case 2: uiDataGrid.ItemsSource = Item.GetUnsoldItems(); break;
                    default: uiDataGrid.ItemsSource = Item.GetAllItems(); break;
                }
                uiTotalItems.Text = string.Format("Total Items = {0}", uiDataGrid.Items.Count);
            }
            catch (Exception)
            {
            }
        }

        private void uiVendorFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (-1 == uiVendorFilter.SelectedIndex)
            {
                return;
            }
            if (0 != uiVendorFilter.SelectedIndex && uiVendorFilter.SelectedIndex != uiVendorFilter.Items.Count - 1)
            {
                string vendor = uiVendorFilter.SelectedValue.ToString();
                uiDataGrid.ItemsSource = Item.GetItemsByVendor(vendor);
                uiTotalItems.Text = string.Format("Total Items = {0}", uiDataGrid.Items.Count);
            }
            if (uiVendorFilter.SelectedIndex == uiVendorFilter.Items.Count - 1 && 0 != uiVendorFilter.SelectedIndex)
            {
                NewVendor newVendor = new NewVendor();
                newVendor.VendorAdded += (a) =>
                    {
                        if (a)
                        {
                            PopulateVendorFilter();
                        }
                    };
                newVendor.Show();
                uiVendorFilter.SelectedIndex = 0;
                PopulateItems();
            }
        }

        private void uiProfitByDate_Click(object sender, RoutedEventArgs e)
        {
            new ProfitByDate().Show();
        }

        private void uiProfitByDateLocation_Click(object sender, RoutedEventArgs e)
        {
            new ProfitByDateLocation().Show();
        }

        private void uiUpdateItemByID_Click(object sender, RoutedEventArgs e)
        {
            UpdateItemByID updateItemByID = new UpdateItemByID();
            updateItemByID.ItemUpdated += (u) =>
                {
                    if (u)
                    {
                        PopulateItems();
                    }
                };
            updateItemByID.Show();
        }
    }
}
