using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class _Default : Page
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

        protected void toUser(object sender, EventArgs e)
        {
            Response.Redirect("UserForms/FRS.aspx");
        }

        protected void toAdmin(object sender, EventArgs e)
        {
            Response.Redirect("AdminForms/AdminService.aspx");
        }
    }
}