using FRS_WorkerRole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace FRS_WorkerRole
{
    /// <summary>
    /// This controller manages the route "api"
    /// it passes http requests to the sql  class
    /// then, send back the requested data
    /// </summary>
    [RoutePrefix("api")]
    public class APIController : ApiController
    {
        private SQL sql = new SQL();
        private NOSQL noSql = new NOSQL();

        [Route("~/api/{db}/airports")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFlights(int db)
        {
            List<Airport> airports = (db == 1) ? await sql.GetAirports() : await noSql.GetAirports();
            
            var jsonmsgs = JsonConvert.SerializeObject(airports);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/airlines")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAirlines(int db)
        {
            List<Airlines> flights = (db == 1) ? await sql.GetAirlines() : await noSql.GetAirlines();

            var jsonmsgs = JsonConvert.SerializeObject(flights);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/getroutes_dep/{dep}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetRoutes_dep(int db, string dep)
        {
            var jsonmsgs = JsonConvert.SerializeObject((db == 1)? await sql.GetRoutes_dep(dep): await noSql.GetRoutes_dep(dep));
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/getpassengers_dep_arr/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetPassengers_dep_arr(int db, int id)
        {
            var jsonmsgs = JsonConvert.SerializeObject((db == 1) ? await sql.GetPassengers_dep_arr(id) : await noSql.GetPassengers_dep_arr(id));
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/getroutes_arr/{arr}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetRoutes_arr(int db, string arr)
        {
            var jsonmsgs = JsonConvert.SerializeObject((db == 1) ? await sql.GetRoutes_arr(arr) : await noSql.GetRoutes_arr(arr));
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }
        
        [Route("~/api/{db}/priceby_flight_date/{id}/{date}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetPriceBy_flight_date(int db, int  id,string date)
        {
            var jsonmsgs = JsonConvert.SerializeObject((db == 1) ? await sql.GetPriceBy_flight_date(id, date) : await noSql.GetPriceBy_flight_date(id, date));
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }
       

        [Route("~/api/{db}/getflights")]
        [Route("~/api/{db}/getflights/{dep}/{arr}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFlights(int db, [FromBody] Flight flight, string dep = null, string arr = null)
        {
            var jsonmsgs = JsonConvert.SerializeObject((db == 1) ? await sql.GetFlights(arr, dep) : await noSql.GetFlights(arr, dep));
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }


        [Route("~/api/{db}/priceby_code/{id}/{code}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetPriceBy_dep(int db, int id, string code)
        {
            var jsonmsgs = JsonConvert.SerializeObject((db == 1) ? await sql.GetPriceBy_code(id, code) : await noSql.GetPriceBy_code(id, code));
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/price")]//test
        [HttpPost]
        public async Task<double> GetPrice(int db, [FromBody] PriceRequest req)
        {
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject(req));
            return await CalPrice(req);
        }

        private async Task<double> CalPrice(PriceRequest req)
        {
            var airport = await sql.GetAirports(req.Code[0]);
            var a1 = airport.ElementAt(0);
            airport = await sql.GetAirports(req.Code[1]);
            var a2 = airport.ElementAt(0); ;
         
            GeoCoordinate firstPoint =
                new GeoCoordinate(a1.latitude, a1.longitude);

            GeoCoordinate secondPoint =
                new GeoCoordinate(a2.latitude, a2.longitude);

            var distance = firstPoint.GetDistanceTo(secondPoint);
            var fare = 0.0;
            distance = distance / 1000.0;

            switch (req.Travellers)
            {
                case "Child":
                    fare  = 0.25 * distance * (1.0 - 0.33);
                    break;
                case "Senior":
                    fare = 0.25 * distance * (1.0 - 0.25);
                    break;
                case "Infant":
                    fare = 0.25 * distance * (1.0 - 0.90);
                    break;                   
                case "Adult":
                    fare = 0.25 * distance;
                    break;    
            }

            return Math.Round(fare, 2);
        }

        [Route("~/api/{db}/holders")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetHolders(int db)
        {
            List<CreditCard> cards = (db == 1) ? await sql.Holders() : await noSql.Holders();

            var jsonmsgs = JsonConvert.SerializeObject(cards);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/transactions/{card?}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTransactions(int db, int card = 0)
        {
            List<Transaction> transactions = (db == 1) ? await sql.GetTransactions(card) :  noSql.GetTransactions(card);

            var jsonmsgs = JsonConvert.SerializeObject(transactions);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/hotels")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetHotels(int db)
        {
            List<Hotel> hotels = (db == 1) ? await sql.GetHotels() : await noSql.GetHotels();

            var jsonmsgs = JsonConvert.SerializeObject(hotels);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/NewTransaction")]
        [HttpPost]
        public async Task<HttpResponseMessage> NewTransaction(int db, [FromBody] Transaction transaction)
        {
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject("transaction!"));
            System.Diagnostics.Trace.TraceInformation(db.ToString());

            if (db == 1)
            {
                await sql.NewTransaction(transaction);
            }
            else
            {
                 noSql.NewTransaction(transaction);
            }

            var jsonmsgs = JsonConvert.SerializeObject("ok");
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/NewCustomer")]
        [HttpPost]
        public async Task<HttpResponseMessage> NewCustomer(int db, [FromBody] Customer customer)
        {
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject("customer!"));
            System.Diagnostics.Trace.TraceInformation(db.ToString());

            if (db == 1)
            {
                await sql.NewCustomer(customer);
            }
            else
            {
                 noSql.NewCustomer(customer);
            }

            var jsonmsgs = JsonConvert.SerializeObject(customer);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }
        
        [Route("~/api/{db}/booking")]
        [HttpPost]
        public async Task<HttpResponseMessage> NewBooking(int db, [FromBody] Booking booking)
        {
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject("booking!"));
            System.Diagnostics.Trace.TraceInformation(db.ToString());

            if (db == 1)
            {
                 await sql.NewBooking(booking);
            }
            else
            {
                  noSql.NewBooking(booking);
            }

            var jsonmsgs = JsonConvert.SerializeObject(booking);
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        [Route("~/api/{db}/InsertToFlight")]
        [HttpPost]
        public async Task<HttpResponseMessage> InsertToFlight(int db, [FromBody] Flight flight)
        {
            System.Diagnostics.Trace.TraceInformation(JsonConvert.SerializeObject("flights!"));
            System.Diagnostics.Trace.TraceInformation(db.ToString());

            if (db == 1)
            {
                await sql.InsertToFlight(flight);
            }
            else
            {
                 noSql.InsertToFlight(flight);
            }

            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent("ok", System.Text.Encoding.UTF8, "application/json");

            return res;
        }
    }
}