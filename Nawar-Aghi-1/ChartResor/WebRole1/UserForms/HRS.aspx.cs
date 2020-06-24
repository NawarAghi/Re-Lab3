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

namespace WebRole1.UserForms
{
    public partial class HRS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlLink css = new System.Web.UI.HtmlControls.HtmlLink();
            css.Href = "/content/flight.css";
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            Master.Page.Header.Controls.Add(css);

            if (!IsPostBack)
            {
                List<Hotel> hotelsList = JsonConvert.DeserializeObject<List<Hotel>>(httpget("hotels"));

                foreach (var a in hotelsList)
                    HotelList.Items.Add(a.Name);
            }
        }

        protected void GetPrice(object sender, EventArgs e)
        {
            PriceLabel.Text = (int.Parse(NightBox.Text) * 200 * 33).ToString();
        }

        private string httpget(string route)
        {
            WebRequest request = WebRequest.Create(
              "http://localhost:80/api/" + databaseOption.SelectedValue + "/" + route);
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


        protected void CancelButton(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void BackButton(object sender, EventArgs e)
        {
            Response.Redirect("FRS.aspx");
        }

        protected void PaymentButton(object sender, EventArgs e)
        {

            Flight f = (Flight)Session["user"];
            List<Hotel> hotelsList = JsonConvert.DeserializeObject<List<Hotel>>(httpget("hotels"));
            var code = 0;

            foreach (var h in hotelsList)
                if (h.Name == HotelList.SelectedValue)
                    code = h.Code;

            Session["hotelbooking"] = new Booking(f.Passport.ToString(), f.Name, DateBox.Text, int.Parse(NightBox.Text), 33, code);
            f.Price += int.Parse(NightBox.Text) * 200 * 33;
            Session["user"] = f;

            Response.Redirect("PS.aspx");
        }
    }
}