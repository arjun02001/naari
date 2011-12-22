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

namespace Naari
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateItems();
        }

        private void PopulateItems()
        {
            uiDataGrid.ItemsSource = Item.GetAllItems();
            uiTotalItems.Text = string.Format("Total Items = {0}", uiDataGrid.Items.Count);
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

        void newItem_NewItemCreated(bool created)
        {
            throw new NotImplementedException();
        }

        private void uiUpdateItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uiDeleteItem_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
