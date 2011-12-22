using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;

namespace Naari.Classes
{
    class Utility
    {
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
