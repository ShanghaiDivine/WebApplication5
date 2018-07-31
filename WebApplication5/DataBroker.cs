using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using s = System.Configuration;
using d = System.Data;
using ss = System.Data.SqlClient;

namespace WebApplication5
{
    public class DataBroker : IDisposable
    {
        public string ConnString { get; set; }
        public DataBroker()
        {
            ConnString = s.ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
        }

        public d.DataTable GetAllProducts()
        {
            var dt = new d.DataTable();
            using (var conn = new ss.SqlConnection(this.ConnString))
            {
                using (var cmd = new ss.SqlCommand("select * from Products", conn))
                {
                    try
                    {
                        conn.Open();
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch (Exception ex)
                    {
                        string exceptionMessage = ex.Message;
                        string innerExceptionMessage = (ex.InnerException is Exception) ? ex.InnerException.Message : "";
                        dt.Rows.Add(dt.NewRow());
                        dt.Columns.Add(new d.DataColumn("ErrorMessage"));
                        dt.Rows[0][0] = exceptionMessage + " " + innerExceptionMessage;
                    }
                }
            }
            return dt;
        }
        public void Dispose()
        {
            ConnString = "";
        }
    }
}