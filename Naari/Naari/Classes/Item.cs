using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;

namespace Naari.Classes
{
    class Item
    {
        public int ID { get; set; }
        public string PurchaseDate { get; set; }
        public string Vendor { get; set; }
        public string BillNumber { get; set; }
        public string ItemName { get; set; }
        public double CostPrice { get; set; }
        public string Location { get; set; }
        public double? SellingPrice { get; set; }
        public string SellingDate { get; set; }

        public static List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                string sql = string.Format(" select * from Naari order by ID desc ");
                items = Utility.PopulateItemsCollection(DataManager.GetData(sql));
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting items");
            }
            return items;
        }

        public static List<Item> GetUnsoldItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                string sql = string.Format(" select * from Naari where SellingPrice is null order by ID desc ");
                items = Utility.PopulateItemsCollection(DataManager.GetData(sql));
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting Unsold items");
            }
            return items;
        }

        public static List<Item> GetSoldItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                string sql = string.Format(" select * from Naari where SellingPrice <> null order by ID desc ");
                items = Utility.PopulateItemsCollection(DataManager.GetData(sql));
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting Sold items");
            }
            return items;
        }
    }
}
