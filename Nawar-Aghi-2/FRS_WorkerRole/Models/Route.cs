using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole
{
    public class Route : TableEntity
    {
        public Route() { }
        public Route(int id, string carrier, string dep, string arr, int flightNmb, string city)
        {
            this.PartitionKey = "Route";

            Id = id;
            Carrier = carrier;
            Dep = dep;
            Arr = arr;
            FlightNmb = flightNmb;
            City = city;
        }

        public int Id{ get;set; }
        public string Carrier{ get;set; }
        public string Dep{ get;set; }
        public string Arr{ get;set; }
        public string City{ get;set; }
        public int FlightNmb{ get;set; }
    }
}
