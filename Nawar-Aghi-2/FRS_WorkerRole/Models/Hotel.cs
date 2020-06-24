using Microsoft.Data.OData.Atom;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole.Models
{
    class Hotel : TableEntity
    {
        public Hotel(){}
        public Hotel(int id, int code, string name, string city, string country, int rate)
        {
            this.PartitionKey = "Hotel";

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