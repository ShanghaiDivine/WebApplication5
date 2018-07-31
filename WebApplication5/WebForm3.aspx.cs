using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var db = new TuesdayDataContext();
            var results = from line in db.Orders
                          where line.Freight < 5
                          select new { line.OrderID, line.OrderDate, line.Freight, line.ShipCountry };

            GridView1.DataSource = results;
            GridView1.DataBind();
        }
    }
}