using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1.Classes;

namespace WebRole1.UserForms
{
    public partial class FRS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlLink css = new System.Web.UI.HtmlControls.HtmlLink();
            css.Href = "/content/flight.css";
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            Master.Page.Header.Controls.Add(css);

            hotelBtn.Enabled = false;
            paymentBtn.Enabled = false;

            if (!IsPostBack)
                Fill_OnPageLoad();
        }

        private void Fill_OnPageLoad()
        {
            List<Airport> airports = JsonConvert.DeserializeObject<List<Airport>>(httpget("airports"));

            foreach (var a in airports)
                FromList.Items.Add(a.code);

            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(airports));
        }

        protected void UpdateToList(object sender, EventArgs e)
        {
            List<Route> routes = JsonConvert.DeserializeObject<List<Route>>(httpget("getroutes_dep/" + FromList.SelectedValue));
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(routes));
            ToList.Items.Clear();

            foreach (var r in routes)
                ToList.Items.Add(r.Arr);
        }

        protected void GetPrice(object sender, EventArgs e)
        {

            PriceLabel.Value = CheckPrice();
            hotelBtn.Enabled = true;
            paymentBtn.Enabled = true;
        }

        private string CheckPrice()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:80/api/" + databaseOption.SelectedValue + "/" + "price");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string[] code = { FromList.SelectedValue, ToList.SelectedValue };
                PriceRequest req = new PriceRequest(code, TypeList.SelectedValue);
                System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(req));

                streamWriter.Write(JsonConvert.SerializeObject(req));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                System.Diagnostics.Trace.TraceInformation(result);
                return result;
            }
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

        protected void HotelBtn(object sender, EventArgs e)
        {
            int[] id = JsonConvert.DeserializeObject<int[]>(httpget("getflights/" + FromList.SelectedValue + "/" + ToList.SelectedValue));

            System.Diagnostics.Trace.TraceInformation(PassengerBox.Text + PassportBox.Text + CheckPrice() + DateTime.Now.ToString("yyyy-MM-dd") + id[0].ToString());


            //Flight flight = new Flight(PassengerBox.Text, int.Parse(PassportBox.Text), double.Parse(CheckPrice()) , DateTime.Now.ToString("yyyy-MM-dd"), id[0]);
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Flight flig = new Flight()
            {
                Name = PassengerBox.Text,
                Passport = int.Parse(PassportBox.Text),
                Price = double.Parse(CheckPrice().Replace(",", ".")),
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Nmb = id[0]
            };

            Session["user"] = flig;
            Response.Redirect("HRS.aspx");

        }

        protected void PaymentBtn(object sender, EventArgs e)
        {
            int[] id = JsonConvert.DeserializeObject<int[]>(httpget("getflights/" + FromList.SelectedValue + "/" + ToList.SelectedValue));

            System.Diagnostics.Trace.TraceInformation(PassengerBox.Text + PassengerBox.Text + CheckPrice() + DateTime.Now.ToString("yyyy-MM-dd") + id[0].ToString());

            Flight flight = new Flight(PassengerBox.Text, int.Parse(PassportBox.Text), double.Parse(CheckPrice().Replace(",", ".")), DateTime.Now.ToString("yyyy-MM-dd"), id[0]);
            
            Session["user"] = flight;
            Response.Redirect("UserForms/PS.aspx");

        }

        protected void CancelBtn(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }


    }
}