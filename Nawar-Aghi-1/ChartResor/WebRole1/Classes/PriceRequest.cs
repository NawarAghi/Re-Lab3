using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class PriceRequest
    {
        public PriceRequest(string[] code, string travellers)
        {
            Code = code;
            Travellers = travellers;
            rate = 200;
        }

        public string[] Code { get; set; }
        public int rate { get; set; }
        public string Travellers { get; set; }
    }
}