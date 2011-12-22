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
    public partial class UpdateItem : Window
    {
        public delegate void ItemUpdatedHandler(bool updated);
        public event ItemUpdatedHandler ItemUpdated;
        Item item;

        public UpdateItem()
        {
            InitializeComponent();
        }

        public UpdateItem(Item item)
        {
            InitializeComponent();
            this.item = item;
            this.DataContext = item;
        }

        private void uiOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uiCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
