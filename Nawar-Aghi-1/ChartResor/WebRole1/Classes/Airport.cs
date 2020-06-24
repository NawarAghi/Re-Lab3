using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Airport
    {
        public Airport(int Id, string Code, string City, double Latitude, double Longitude)
        {
            id = Id;
            code = Code;
            city = City;
            latitude = Latitude;
            longitude = Longitude;
        }
        public int id { get; set; }
        public string code { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}