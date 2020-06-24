using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Booking
    {
        public Booking(string pass, string name, string arr_date, int nmb_nights, double fee, int code)
        {
            Pass = pass;
            Name = name;
            Arr_date = arr_date;
            Nmb_nights = nmb_nights;
            Fee = fee;
            Code = code;
        }

        public string Pass { get; set; }
        public string Name { get; set; }
        public string Arr_date { get; set; }
        public int Nmb_nights { get; set; }
        public double Fee { get; set; }
        public int Code { get; set; }
    }
}