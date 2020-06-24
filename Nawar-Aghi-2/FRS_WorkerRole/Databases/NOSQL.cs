using FRS_WorkerRole.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FRS_WorkerRole
{
    /// <summary>
    /// this class handles the given requests, fetch data from db, and return back the data
    /// </summary>
    class NOSQL
    {
        private string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storagelab3nosql;AccountKey=3Jpx1nxltPkUhXbbqbx5qAOVTvh6RB7DnEkh/DLnb/rLcyTf4IRPTnZ1OekHCq17L6rtDd5d+H7kyaL0Srn5qQ==;EndpointSuffix=core.windows.net";
        private CloudStorageAccount cloudStorageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable table;
        private SQL sql = new SQL();

        public NOSQL()
        {
            // establish communication with the cloud storage and creates a table in case it doesn't exit
            cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            table = cloudTableClient.GetTableReference("servicesTable");
            table.CreateIfNotExists();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<List<Airport>> GetAirports(string code = null)
        {
            List<Airport> airports = new List<Airport>();

            var entities = table.ExecuteQuery(new TableQuery<Airport>()).ToList();
            foreach (Airport entity in entities)
            {
                if (entity.PartitionKey == "Airport")
                {
                    if (code != null)
                        if (code != entity.code)
                            continue;

                    airports.Add(entity);
                }
            }
            return airports;
        }

        /// <summary>
        /// get all  airlines
        /// </summary>
        /// <returns></returns>
        public async Task<List<Airlines>> GetAirlines()
        {
            List<Airlines> airlines = new List<Airlines>();

            var entities = table.ExecuteQuery(new TableQuery<Airlines>()).ToList();
            foreach (Airlines entity in entities)
            {
                if (entity.PartitionKey == "Airlines")
                {
                    airlines.Add(entity);
                }
            }
            return airlines;
        }
        /// <summary>
        /// get all routes for a specific departure
        /// </summary>
        /// <param name="dep">departure code</param>
        /// <returns></returns>
        public async Task<List<Route>> GetRoutes_dep(string dep)
        {
            List<Route> routes = new List<Route>();
            List<Airport> airports = await GetAirports();

            var entities = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            foreach (Route entity in entities)
            {
                if (entity.PartitionKey == "Route")
                {
                    foreach (var a in airports)
                        if (entity.Dep == a.code)
                            entity.City = a.city;

                    if (entity.Dep == dep)
                        routes.Add(entity);
                }
            }

            return routes;
        }
        /// <summary>
        /// get all passengers and departure/arrival city
        /// </summary>
        /// <param name="id">flight number</param>
        /// <returns></returns>
        public async Task<List<Passenger>> GetPassengers_dep_arr(int id)
        {
            List<Passenger> passengers = new List<Passenger>();
            List<Route> routes = new List<Route>();

            var entitiees = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            foreach (Route entity in entitiees)
            {
                if (entity.PartitionKey == "Route")
                {
                    routes.Add(entity);
                }
            }

            var entities = table.ExecuteQuery(new TableQuery<Flight>()).ToList();
            foreach (Flight entity in entities)
            {
                if (entity.PartitionKey == "Flight")
                {
                    if (entity.Nmb == id)
                    {
                        string dep = "";
                        string arr = "";

                        foreach (var r in routes)
                            if (r.FlightNmb == id)
                            {
                                dep = r.Dep;
                                arr = r.Arr;
                                break;
                            }

                        passengers.Add(new Passenger(entity.Name, dep, arr));
                    }
                }
            }

            return passengers;
        }
        /// <summary>
        /// prices by flight number and date
        /// </summary>
        /// <param name="id">flight number</param>
        /// <param name="date">departure date</param>
        /// <returns></returns>
        public async Task<List<Airlines>> GetPriceBy_flight_date(int id, string date)
        {
            List<Airlines> prices = new List<Airlines>();
            List<Route> routes = new List<Route>();
            List<Airport> airports = await GetAirports();

            var entitiees = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            foreach (Route entity in entitiees)
            {
                if (entity.PartitionKey == "Route")
                {
                    routes.Add(entity);
                }
            }

            var entities = table.ExecuteQuery(new TableQuery<Flight>()).ToList();
            foreach (Flight entity in entities)
            {
                if (entity.PartitionKey == "Flight")
                {
                    Trace.TraceInformation(DateTime.Parse(entity.Date).ToString("yyyy-MM-dd"));

                    if (entity.Nmb == id && DateTime.Parse(entity.Date).ToString("yyyy-MM-dd") == date) // check data !!TODO
                    {
                        string dep = "";
                        string arr = "";

                        foreach (var r in routes)
                            if (r.FlightNmb == id)
                            {
                                dep = r.Dep;
                                arr = r.Arr;
                                break;
                            }

                        foreach (var a in airports)
                        {
                            if (dep == a.code)
                                dep = a.city;
                            if (arr == a.code)
                                arr = a.city;
                        }

                        prices.Add(new Airlines(int.Parse(entity.Price.ToString()), dep, arr));
                    }
                }
            }

            return prices;
        }
        /// <summary>
        /// get prices by departure/arrival city code
        /// </summary>
        /// <param name="code">city code</param>
        /// <returns></returns>
        public async Task<List<double>> GetPriceBy_code(int id, string code)
        {
            List<double> prices = new List<double>();
            List<int> flightnmbr = new List<int>();

            var entitiees = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            foreach (Route entity in entitiees)
            {
                if (entity.PartitionKey == "Route")
                {
                    if (id == 1)
                    {
                        if (entity.Dep == code)
                        {
                            flightnmbr.Add(entity.FlightNmb);
                        }
                    }
                    else
                    {
                        if (entity.Arr == code)
                        {
                            flightnmbr.Add(entity.FlightNmb);
                        }
                    }
                }
            }

            var entities = table.ExecuteQuery(new TableQuery<Flight>()).ToList();
            foreach (Flight entity in entities)
            {
                if (entity.PartitionKey == "Flight")
                    foreach (var e in flightnmbr)
                        if (entity.Nmb == e)
                            prices.Add(entity.Price);
            }

            return prices;
        }
        /// <summary>
        /// get routes for a specific arrival city 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public async Task<List<Route>> GetRoutes_arr(string arr)
        {
            List<Route> routes = new List<Route>();
            List<Airport> airports = await GetAirports();

            var entities = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            foreach (Route entity in entities)
            {
                if (entity.PartitionKey == "Route")
                {
                    foreach (var a in airports)
                        if (entity.Arr == a.code)
                            entity.City = a.city;

                    if (entity.Arr == arr)
                        routes.Add(entity);
                }
            }

            return routes;
        }
        /// <summary>
        /// get all flight from database
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="dep"></param>
        /// <returns></returns>
        public async Task<List<int>> GetFlights(string arr = null, string dep = null)
        {
            List<int> flights = new List<int>();

            var entities = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            foreach (Route entity in entities)
            {
                if (entity.PartitionKey == "Route")
                {
                    if (arr != null)
                    {
                        if (arr.ToLower() == entity.Arr.ToLower() && dep.ToLower() == entity.Dep.ToLower())
                        {
                            flights.Add(entity.FlightNmb);

                        }
                    }
                    else
                    {
                        flights.Add(entity.FlightNmb);
                    }
                }
            }

            return flights;
        }
        /// <summary>
        /// add a new flight to database
        /// </summary>
        /// <param name="flight"></param>
        public void InsertToFlight(Flight flight)
        {
            string rowKey = Guid.NewGuid().ToString("N");
            flight.RowKey = rowKey;
            TableOperation insert = TableOperation.Insert(flight);
            table.Execute(insert);
        }
        /// <summary>
        /// get all customers' credentials
        /// </summary>
        /// <returns></returns>
        public async Task<List<CreditCard>> Holders()
        {
            List<CreditCard> cards = new List<CreditCard>();

            var entities = table.ExecuteQuery(new TableQuery<CreditCard>()).ToList();
            foreach (CreditCard entity in entities)
            {
                if (entity.PartitionKey == "Customer")
                {

                    cards.Add(entity);
                }
            }
            return cards;
        }
        
        public List<Transaction> GetTransactions(int card = 0)
        {
            List<Transaction> transactions = new List<Transaction>();

            var entities = table.ExecuteQuery(new TableQuery<Transaction>()).ToList();
            foreach (Transaction entity in entities)
            {
                if (entity.PartitionKey == "Transaction")
                {
                    if (card != 0)
                    {
                        if (card == entity.Card)
                            transactions.Add(entity);
                    }
                    else
                    {
                        transactions.Add(entity);
                    }
                }
            }

            return transactions;
        }
        
        public async Task<List<Hotel>> GetHotels()
        {
            List<Hotel> hotels = new List<Hotel>();

            var entities = table.ExecuteQuery(new TableQuery<Hotel>()).ToList();
            foreach (Hotel entity in entities)
            {
                if (entity.PartitionKey == "Hotel")
                {

                    hotels.Add(entity);
                }
            }
            return hotels;
        }
        
        public void NewCustomer(Customer customer)
        {
            string rowKey = Guid.NewGuid().ToString("N");
            customer.RowKey = rowKey;
            TableOperation insert = TableOperation.Insert(customer);
            table.Execute(insert);
        }
       
        public void NewTransaction(Transaction transaction)
        {
            string rowKey = Guid.NewGuid().ToString("N");
            transaction.RowKey = rowKey;
            TableOperation insert = TableOperation.Insert(transaction);
            table.Execute(insert);
        }
        /// <summary>
        /// add a new booking to database
        /// </summary>
        /// <param name="booking"></param>
        public void NewBooking(Booking booking)
        {
            string rowKey = Guid.NewGuid().ToString("N");
            booking.RowKey = rowKey;
            TableOperation insert = TableOperation.Insert(booking);
            table.Execute(insert);
        }

        
    }
}
