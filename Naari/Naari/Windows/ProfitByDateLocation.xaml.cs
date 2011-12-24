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
    public partial class ProfitByDateLocation : Window
    {
        public ProfitByDateLocation()
        {
            InitializeComponent();
            PopulateLocations();
        }

        private void PopulateLocations()
        {
            try
            {
                string sql = "select distinct(Location) from Naari order by Location asc";
                DataTable dt = DataManager.GetData(sql);
                uiLocation.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Location"].ToString()))
                    {
                        uiLocation.Items.Add(dr["Location"].ToString());
                    }
                }
                if (uiLocation.Items.Count > 0)
                {
                    uiLocation.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting locations");
            }
        }

        private void uiSubmit_Click(object sender, RoutedEventArgs e)
        {
            List<string> locations = new List<string>();
            List<Models.ProfitByDate> profits = new List<Models.ProfitByDate>();
            List<KeyValuePair<string, double>> valueList = new List<KeyValuePair<string, double>>();
            double totalCostPrice = 0, totalSellingPrice = 0;

            if (null == uiFromDate.Value || null == uiToDate.Value)
            {
                MessageBox.Show("Please enter a date range");
                return;
            }
            foreach (var str in uiLocation.SelectedItems)
            {
                locations.Add(str.ToString());
            }
            if (locations.Count == 0)
            {
                MessageBox.Show("Please select location/locations");
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format(" select Vendor, sum(CostPrice) as SumCostPrice, sum(SellingPrice) as SumSellingPrice "));
                sb.Append(string.Format(" , sum(SellingPrice) - sum(CostPrice) as Profit, count(*) as ItemsSold "));
                sb.Append(string.Format(" from Naari where SellingDate >= #{0}# and SellingDate <= #{1}# ", uiFromDate.Value, uiToDate.Value));
                sb.Append(string.Format(" and Location in ( "));
                for(int i = 0; i < locations.Count - 1; i++)
                {
                    sb.Append(string.Format(" '{0}', ", locations[i]));
                }
                sb.Append(string.Format(" '{0}' ", locations[locations.Count - 1]));
                sb.Append(string.Format(" ) group by Vendor order by 4 desc "));
                DataTable dt = DataManager.GetData(sb.ToString());
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
