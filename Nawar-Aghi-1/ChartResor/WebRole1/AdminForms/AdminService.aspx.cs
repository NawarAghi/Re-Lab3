using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1.Classes;

namespace WebRole1.AdminForms
{
    public partial class AdminService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Adding CSS file to this web form
            System.Web.UI.HtmlControls.HtmlLink css = new System.Web.UI.HtmlControls.HtmlLink();
            css.Href = "/content/flight.css";
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            Master.Page.Header.Controls.Add(css);

          
        }

        protected void AirlinePage(object sender, EventArgs e)
        {
            Response.Redirect("AirlineService.aspx");
        }

        protected void AirportPage(object sender, EventArgs e)
        {
            Response.Redirect("AirportService.aspx");
        }

        protected void RouteDepPage(object sender, EventArgs e)
        {
            Response.Redirect("RouteService.aspx");
        }

        protected void RouteArrPage(object sender, EventArgs e)
        {
            Response.Redirect("RouteArriving.aspx");
        }

        protected void RevenuePage(object sender, EventArgs e)
        {
            Response.Redirect("RevenueService.aspx");
        }

        protected void CardHolderPage(object sender, EventArgs e)
        {
            Response.Redirect("CardHolderService.aspx");
        }

        protected void TransactionsPage(object sender, EventArgs e)
        {
            Response.Redirect("Transactions.aspx");
        }
    }
}