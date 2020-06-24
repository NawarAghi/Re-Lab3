using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using WebRole1.Classes;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebRole1.UserForms
{
    public partial class PS : System.Web.UI.Page
    {

        private HttpClient client = new HttpClient();
        public Flight flight;
        public Booking booking;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlLink css = new System.Web.UI.HtmlControls.HtmlLink();
            css.Href = "/content/flight.css";
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            Master.Page.Header.Controls.Add(css);

            OnPageLoad();
        }


        private void OnPageLoad()
        {
            try
            {
                flight = (Flight)Session["user"];
                PriceLabel.Text = flight.Price.ToString();

                if (Session["hotelbooking"] != null)
                    booking = (Booking)Session["hotelbooking"];
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }



        protected void CancelButton(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void BackButton(object sender, EventArgs e)
        {
            Response.Redirect("HRS.aspx");
        }

        protected void PaymentButton(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(flight));
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(booking));
            var flightTask = client.PostAsync("http://localhost:80/api/" + databaseOption.SelectedValue + "/" + "InsertToFlight", new StringContent(
                   JsonConvert.SerializeObject(flight),
                   Encoding.UTF8,
                   "application/json")
                   );
            Task.WaitAll(flightTask);

            if (booking != null)
            {
                System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(booking));
                var bookingTask = client.PostAsync("http://localhost:80/api/" + databaseOption.SelectedValue + "/booking", new StringContent(
                       JsonConvert.SerializeObject(booking),
                       Encoding.UTF8,
                       "application/json")
                       );
                Task.WaitAll(bookingTask);
            }

            Customer customer = new Customer(int.Parse(CreditBox.Text), HolderBox.Text, DateBox.Text, double.Parse(BalanceBox.Text));
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(customer));
            var customerTask = client.PostAsync("http://localhost:80/api/" + databaseOption.SelectedValue + "/" + "NewCustomer", new StringContent(
                   JsonConvert.SerializeObject(customer),
                   Encoding.UTF8,
                   "application/json")
                   );
            Task.WaitAll(customerTask);

            Transaction transaction = new Transaction(DateTime.Now.ToString("yyyy-MM-dd"), int.Parse(CreditBox.Text), flight.Price);
            var transactionTask = client.PostAsync("http://localhost:80/api/" + databaseOption.SelectedValue + "/" + "NewTransaction", new StringContent(
                   JsonConvert.SerializeObject(transaction),
                   Encoding.UTF8,
                   "application/json")
                   );

            Task.WaitAll(transactionTask);

            Response.Redirect("~/Default.aspx");
        }
    }
}