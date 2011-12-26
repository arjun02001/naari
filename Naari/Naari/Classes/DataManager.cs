//Arjun Mukherji
using System;
using System.Data.OleDb;
using System.Data;
using System.Windows;

namespace Naari.Classes
{
    class DataManager
    {
        const string connectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=Naari.accdb;";

        public static DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    using(OleDbDataAdapter da = new OleDbDataAdapter(sql, con))
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting data from database");
            }
            return dt;
        }

        public static bool SetData(string sql)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    using (OleDbCommand com = new OleDbCommand(sql, con))
                    {
                        return (com.ExecuteNonQuery() > 0) ? true : false;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while setting data to database");
            }
            return false;
        }
    }
}
