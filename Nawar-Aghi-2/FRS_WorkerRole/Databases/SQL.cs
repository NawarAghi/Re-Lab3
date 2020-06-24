using FRS_WorkerRole.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FRS_WorkerRole
{
    /// <summary>
    /// This class fetch data from the db and return it with
    /// the apropriate  datatype so it can be sent back to client
    /// </summary>
    class SQL
    {
        MySqlConnectionStringBuilder builder;

        public SQL()
        {
            builder = new MySqlConnectionStringBuilder
            {
                Server = "lab3nawar.mysql.database.azure.com",
                Database = "services",
                UserID = "nawar@lab3nawar",
                Password = "Nano.123",
                SslMode = MySqlSslMode.Required,
            };
        }

        public async Task<List<Flight>> GetFlightss()
        {
            List<Flight> flights = new List<Flight>();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM flights; ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flights.Add(new Flight(reader.GetString(0),
                                reader.GetInt32(1), reader.GetDouble(2), 
                                reader.GetString(3), reader.GetInt32(4)));
                        }
                    }

                }

            }

            return flights;
        }

        public async Task<List<Booking>> GetBooking()
        {
            List<Booking> bookings = new List<Booking>();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM booking;";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bookings.Add(new Booking(reader.GetString(0), 
                                reader.GetString(1), reader.GetString(2), 
                                reader.GetInt32(3), reader.GetDouble(4), reader.GetInt32(5)));
                        }
                    }

                }

            }

            return bookings;
        }

        /// <summary>
        /// get all airports
        /// </summary>
        /// <param name="code">airport code</param>
        /// <returns>list of airports entries</returns>
        public async Task<List<Airport>> GetAirports(string code = null)
        {
            List<Airport> flights = new List<Airport>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM airports " + ((code != null) ? "WHERE code =@code" : "") + ";";
                    command.Parameters.AddWithValue("@code", code);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flights.Add(new Airport(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3),
                                reader.GetDouble(4)));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return flights;
        }
        /// <summary>
        /// get all  airlines
        /// </summary>
        /// <returns></returns>
        public async Task<List<Airlines>> GetAirlines()
        {
            List<Airlines> airlines = new List<Airlines>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM airlines;";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            airlines.Add(new Airlines(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
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

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT routes.id, routes.carrier, routes.dep, routes.arr, routes.flight_nmb, airports.city FROM routes left join airports on routes.dep = airports.code WHERE dep = @dep;";
                    command.Parameters.AddWithValue("@dep", dep);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            routes.Add(new Route(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetString(5)
                                ));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
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

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Distinct  * FROM" +
                        " ( select flights.Passenger_name as name from flights where flights.flight_nmb  =  @id) as name " +
                        ",(select airports.city as dep from airports, routes where  (select routes.dep  from routes where flight_nmb = @id) = airports.code) as dep" +
                        ",(select airports.city as arr from airports , routes  where (select routes.arr from routes where flight_nmb = @id)  = airports.code) as arr;";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            passengers.Add(new Passenger(reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2)
                                ));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
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

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {///2222-03-22
                    command.CommandText = "SELECT Distinct * FROM (select flights.air_fare as id from flights where flights.flight_nmb = @id AND flights.dep_date = @date)  as price" +
                        ", ( select airports.city as code from airports, routes where (select routes.dep from routes where routes.flight_nmb = @id) = airports.code) as dep" +
                        ",( select airports.city as name from airports, routes where (select routes.arr from routes where routes.flight_nmb = @id) = airports.code)  as arr ;";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@date", date);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            prices.Add(new Airlines(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                                ));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
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

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {///2222-03-22
                    command.CommandText = "select Distinct * from (select flights.air_fare as price from flights where flights.flight_nmb in (select routes.flight_nmb from routes where routes." + ((id == 1) ? "dep" : "arr") + "= @code ))  as name";
                    command.Parameters.AddWithValue("@code", code);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            prices.Add(reader.GetDouble(0));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }

            return prices;
        }
        /// <summary>
        /// get routes by arrival city
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public async Task<List<Route>> GetRoutes_arr(string arr)
        {
            List<Route> routes = new List<Route>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT routes.id, routes.carrier, routes.dep, routes.arr, routes.flight_nmb, airports.city FROM routes left join airports on routes.arr = airports.code WHERE arr = @arr";
                    command.Parameters.AddWithValue("@arr", arr);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            routes.Add(new Route(reader.GetInt32(0),
                               reader.GetString(1),
                               reader.GetString(2),
                               reader.GetString(3),
                               reader.GetInt32(4),
                               reader.GetString(5)
                               ));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }

            return routes;
        }
        /// <summary>
        /// get price of all flights or flights' price by arrival or departure city
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="dep"></param>
        /// <returns></returns>
        public async Task<List<int>> GetFlights(string arr = null, string dep = null)
        {
            List<int> flights = new List<int>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT routes.flight_nmb  FROM routes " + ((arr != null) ? "WHERE arr = @arr AND dep = @dep;" : ";");
                    command.Parameters.AddWithValue("@arr", arr);
                    command.Parameters.AddWithValue("@dep", dep);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flights.Add(reader.GetInt32(0));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return flights;
        }
        /// <summary>
        /// insert a new flight element
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public async Task<int> InsertToFlight(Flight flight)
        {
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO flights  (Passenger_name, Passport_nmb, dep_date, flight_nmb, air_fare) VALUES (@name, @passport, @date, @flight, @price)";
                    command.Parameters.AddWithValue("@name", flight.Name);
                    command.Parameters.AddWithValue("@passport", flight.Passport);
                    command.Parameters.AddWithValue("@date", flight.Date);
                    command.Parameters.AddWithValue("@flight", flight.Nmb);
                    command.Parameters.AddWithValue("@price", flight.Price);

                    var reader = await command.ExecuteReaderAsync();
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return 1;
        }
        /// <summary>
        /// to get all customers details
        /// </summary>
        /// <returns></returns>
        public async Task<List<CreditCard>> Holders()
        {
            List<CreditCard> cards = new List<CreditCard>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = ("SELECT *  FROM customer");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cards.Add(new CreditCard(reader.GetInt32(0)
                                        , reader.GetInt32(1)
                                        , reader.GetString(2)
                                        , reader.GetString(3)
                                        , reader.GetDouble(4)));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return cards;
        }
        /// <summary>
        /// get transactions or transactions for a specific card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public async Task<List<Transaction>> GetTransactions(int card = 0)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = ("SELECT *  FROM transactions " + ((card == 0) ? "" : "WHERE card = @card"));
                    command.Parameters.AddWithValue("@card", card);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            transactions.Add(new Transaction(reader.GetString(1)
                                        , reader.GetInt32(2)
                                        , reader.GetDouble(3)));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return transactions;
        }
        /// <summary>
        /// get all hotels
        /// </summary>
        /// <returns></returns>
        public async Task<List<Hotel>> GetHotels()
        {
            List<Hotel> transactions = new List<Hotel>();

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = ("SELECT *  FROM hotel");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            transactions.Add(new Hotel(reader.GetInt32(0)
                                        , reader.GetInt32(1)
                                        , reader.GetString(2)
                                        , reader.GetString(3)
                                        , reader.GetString(4)
                                        , reader.GetInt32(5)));
                        }
                    }
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return transactions;
        }
        /// <summary>
        /// add a new customer to database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<int> NewCustomer(Customer customer)
        {
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO customer  (card, name, ex_date, balance) VALUES (@card, @name, @ex_date, @balance)";
                    command.Parameters.AddWithValue("@name", customer.Name);
                    command.Parameters.AddWithValue("@ex_date", customer.Ex_date);
                    command.Parameters.AddWithValue("@balance", customer.Balance);
                    command.Parameters.AddWithValue("@card", customer.Card);

                    var reader = await command.ExecuteReaderAsync();
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return 1;
        }
        /// <summary>
        /// add a new transaction to db
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<int> NewTransaction(Transaction transaction)
        {
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO transactions  (date, card, amount) VALUES (@date, @card, @amount)";
                    command.Parameters.AddWithValue("@date", transaction.Date);
                    command.Parameters.AddWithValue("@card", transaction.Card);
                    command.Parameters.AddWithValue("@amount", transaction.Amount);

                    var reader = await command.ExecuteReaderAsync();
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
            return 1;
        }
        /// <summary>
        /// add a new booking to db
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task NewBooking(Booking booking)
        {
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                System.Diagnostics.Trace.TraceInformation("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO booking (pass, name, arr_date, nmb_nights, fee, code) VALUES (@pass, @name, @arr_date, @nmb_nights, @fee, @code)";
                    command.Parameters.AddWithValue("@pass", booking.Pass);
                    command.Parameters.AddWithValue("@name", booking.Name);
                    command.Parameters.AddWithValue("@arr_date", booking.Arr_date);
                    command.Parameters.AddWithValue("@nmb_nights", booking.Nmb_nights);
                    command.Parameters.AddWithValue("@fee", booking.Fee);
                    command.Parameters.AddWithValue("@code", booking.Code);

                    var reader = await command.ExecuteReaderAsync();
                }
                System.Diagnostics.Trace.TraceInformation("Closing connection");
            }
        }
    }
}