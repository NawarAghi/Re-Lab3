using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole.Models
{
    public class Customer : TableEntity
    {
        public Customer(int card, string name, string ex_date, double balance)
        {
            this.PartitionKey = "Customer";

            Card = card;
            Name = name;
            Ex_date = ex_date;
            Balance = balance;
        }

        public int Card { set; get; }
        public string Name { set; get; }
        public string Ex_date { set; get; }
        public double Balance { set; get; }
    }
}
