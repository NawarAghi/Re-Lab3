using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Flight
    {
        public Flight(string name, int passport, double price, string date, int nmb)
        {
            Name = name;
            Passport = passport;
            Price = price;
            Date = date;
            Nmb = nmb;
        }

        public Flight() { }
        public string Name { set; get; }
        public int Nmb { set; get; }

        public int Passport { set; get; }
        public double Price { set; get; }
        public string Date { set; get; }
    }
}