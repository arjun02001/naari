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
        double totalCostPrice, totalSellingPrice;

        public ProfitByDate()
        {
            InitializeComponent();
        }

        private void uiSubmit_Click(object sender, RoutedEventArgs e)
        {
            List<Models.ProfitByDate> profits = new List<Models.ProfitByDate>();
            List<KeyValuePair<string, double>> valueList = new List<KeyValuePair<string, double>>();
            if (null == uiFromDate.Value || null == uiToDate.Value)
            {
                MessageBox.Show("Please enter a date range");
                return;
            }
            try
            {
                string sql = string.Format(" select Vendor, sum(CostPrice) as SumCostPrice, sum(SellingPrice) as SumSellingPrice ") +
                                string.Format(" , sum(SellingPrice) - sum(CostPrice) as Profit, count(*) as ItemsSold ") +
                                string.Format(" from Naari where SellingDate >= #{0}# and SellingDate <= #{1}# group by Vendor order by 4 desc ", uiFromDate.Value, uiToDate.Value);
                DataTable dt = DataManager.GetData(sql);
                totalCostPrice = totalSellingPrice = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    profits.Add(new Models.ProfitByDate()
                        {
                            Vendor = dr["Vendor"].ToString(),
                            SumCostPrice = Convert.ToDouble(dr["SumCostPrice"]),
                            SumSellingPrice = Convert.ToDouble(dr["SumSellingPrice"]),
                            Profit = Convert.ToDouble(dr["Profit"].ToString()),
                            ItemsSold = Convert.ToInt32(dr["ItemsSold"])
                        });
                    totalCostPrice += Convert.ToDouble(dr["SumCostPrice"]);
                    totalSellingPrice += Convert.ToDouble(dr["SumSellingPrice"]);
                    valueList.Add(new KeyValuePair<string, double>(dr["Vendor"].ToString(), Convert.ToDouble(dr["Profit"])));
                }
                uiDataGrid.ItemsSource = profits;
                uiTotalCostPrice.Text = string.Format("TotalCostPrice = {0}", totalCostPrice);
                uiTotalSellingPrice.Text = string.Format("TotalSellingPrice = {0}", totalSellingPrice);
                uiTotalProfit.Text = string.Format("TotalProfit = {0}", totalSellingPrice - totalCostPrice);
                uiPieChart.ItemsSource = valueList;
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting data");
            }
        }
    }
}
