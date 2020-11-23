using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GridView1.DataSource = service.GetData(null, null);
                GridView1.DataBind();
            }
        }
        protected void GetData_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dateFrom.Text) && !string.IsNullOrEmpty(dateTo.Text) && DateTime.Parse(dateFrom.Text) < DateTime.Parse(dateTo.Text))
            {
                GridView1.DataSource = service.GetData(dateFrom.Text, dateTo.Text);
                GridView1.DataBind();
            }
            else
            {
                Page_Load(null, null);
            }
        }
    }
}