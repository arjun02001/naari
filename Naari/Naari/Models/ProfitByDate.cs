using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naari.Models
{
    class ProfitByDate
    {
        public string Vendor { get; set; }
        public double SumCostPrice { get; set; }
        public double SumSellingPrice { get; set; }
        public double Profit { get; set; }
        public int ItemsSold { get; set; }
    }
}
