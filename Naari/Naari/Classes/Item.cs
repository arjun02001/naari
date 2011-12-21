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
                string sql = string.Format(" select * from Naari ");
                DataTable dt = DataManager.GetData(sql);
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
                MessageBox.Show("Error while getting items");
            }
            return items;
        }
    }
}
