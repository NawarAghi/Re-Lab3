using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Route
    {
        public Route(int id, string carrier, string dep, string arr, int flightNmb, string city)
        {
            Id = id;
            Carrier = carrier;
            Dep = dep;
            Arr = arr;
            FlightNmb = flightNmb;
            City = city;
        }

        public int Id { get; set; }
        public string Carrier { get; set; }
        public string Dep { get; set; }
        public string Arr { get; set; }
        public string City { get; set; }
        public int FlightNmb { get; set; }
    }
}