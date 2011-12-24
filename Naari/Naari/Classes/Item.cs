using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;

namespace Naari.Classes
{
    public class Item
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

        public static List<Item> GetItemsByVendor(string vendor)
        {
            List<Item> items = new List<Item>();
            try
            {
                string sql = string.Format(" select * from Naari where Vendor = '{0}' ", vendor);
                items = Utility.PopulateItemsCollection(DataManager.GetData(sql));
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting items by vendor");
            }
            return items;
        }

        public static Item GetItemByID(int id)
        {
            List<Item> items = new List<Item>();
            try
            {
                string sql = string.Format(" select * from Naari where ID = {0} ", id);
                items = Utility.PopulateItemsCollection(DataManager.GetData(sql));
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting item by id");
            }
            return (items.Count > 0) ? items[0] : null;
        }

        public static List<Item> PopulateItemsCollection(DataTable dt)
        {
            List<Item> items = new List<Item>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    items.Add(new Item()
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]).ToShortDateString(),
                        Vendor = dr["Vendor"].ToString(),
                        BillNumber = dr["BillNumber"].ToString(),
                        ItemName = dr["ItemName"].ToString(),
                        CostPrice = Convert.ToDouble(dr["CostPrice"]),
                        Location = (dr["Location"] != DBNull.Value) ? dr["Location"].ToString() : string.Empty,
                        SellingPrice = (dr["SellingPrice"] != DBNull.Value) ? Convert.ToDouble(dr["SellingPrice"]) : (double?)null,
                        SellingDate = (dr["SellingDate"] != DBNull.Value) ? Convert.ToDateTime(dr["SellingDate"]).ToShortDateString() : string.Empty
                    });
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while populating items collection");
            }
            return items;
        }
    }
}
