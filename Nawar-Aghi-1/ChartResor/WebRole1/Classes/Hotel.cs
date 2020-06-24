using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Hotel
    {
        public Hotel(int id, int code, string name, string city, string country, int rate)
        {
            Id = id;
            Code = code;
            Name = name;
            City = city;
            Country = country;
            Rate = rate;
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Rate { get; set; }
    }
}