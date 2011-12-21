using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naari.Classes
{
    class Item
    {
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Vendor { get; set; }
        public string BillNumber { get; set; }
        public string ItemName { get; set; }
        public double CostPrice { get; set; }
        public string Location { get; set; }
        public double SellingPrice { get; set; }
        public DateTime SellingDate { get; set; }
    }
}
