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
using System.Data;
using Naari.Classes;

namespace Naari.Windows
{
    public partial class ProfitByDate : Window
    {
        double totalCostPrice, totalSellingPrice, profit;

        public ProfitByDate()
        {
            InitializeComponent();
        }

        private void uiSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (null == uiFromDate.Value || null == uiToDate.Value)
            {
                MessageBox.Show("Please enter a date range");
                return;
            }
            try
            {
                string sql = string.Format(" select Vendor, sum(CostPrice) as SumCostPrice, sum(SellingPrice) as SumSellingPrice ") +
                                string.Format(" , sum(SellingPrice) - sum(CostPrice) as Profit, count(*) as ItemsSold ") +
                                string.Format(" from Naari where SellingDate >= #{0}# and SellingDate <= #{1}# group by Vendor ", uiFromDate.Value, uiToDate.Value);
                DataTable dt = DataManager.GetData(sql);
                foreach (DataRow dr in dt.Rows)
                {
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting data");
            }
        }
    }
}
