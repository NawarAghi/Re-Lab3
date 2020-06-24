using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1.Classes;

namespace WebRole1.AdminForms
{
    public partial class RevenueService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Adding CSS file to this web form
            System.Web.UI.HtmlControls.HtmlLink css = new System.Web.UI.HtmlControls.HtmlLink();
            css.Href = "/content/flight.css";
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            Master.Page.Header.Controls.Add(css);

            List<int> flights = JsonConvert.DeserializeObject<List<int>>(httpget(int.Parse(databaseOption.SelectedValue), "getflights"));
            foreach (var f in flights)
            {
                flightList.Items.Add(f.ToString());
                
            }
        }

        private string httpget(int database, string route)
        {
            WebRequest request = WebRequest.Create(
              "http://localhost:80/api/" + database + "/" + route);
            request.Credentials = CredentialCache.DefaultCredentials;
            string responseFromServer;

            WebResponse response = request.GetResponse();
            System.Diagnostics.Trace.TraceInformation(((HttpWebResponse)response).StatusDescription);

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            response.Close();

            return responseFromServer;
        }

        protected void Back(object sender, EventArgs e)
        {
            Response.Redirect("AdminService.aspx");
        }

        protected void FetchDat(object sender, EventArgs e)
        {
            List<Airline> passengers = JsonConvert.DeserializeObject<List<Airline>>(httpget(int.Parse(databaseOption.SelectedValue), "priceby_flight_date/" + flightList.SelectedValue + "/" + date.Text));
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(passengers));
            RevenueTable.DataSource = passengers;
            RevenueTable.AutoGenerateColumns = false;
            RevenueTable.DataBind();
        }
    }
}