using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole.Models
{
    public class Flight : TableEntity
    {
        public Flight() {  }
        public Flight(string name, int passport, double price, string date, int nmb)
        {
            this.PartitionKey = "Flight";

            Name = name;
            Passport = passport;
            Price = price;
            Date = date;
            Nmb = nmb;
        }

        public string Name { set; get; }
        public int Nmb { set; get; }
        public int Passport { set; get; }
        public double Price { set; get; }
        public string Date { set; get; }
    }
}
