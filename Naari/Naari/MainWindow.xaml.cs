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
        }

        private void uiAddNewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uiUpdateItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uiDeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
