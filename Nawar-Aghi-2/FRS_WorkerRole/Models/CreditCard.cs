using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole.Models
{
    class CreditCard : TableEntity
    {
        public CreditCard() { }
        public CreditCard(int Id, int Card, string Name, string Ex_date, double Balance)
        {
            this.PartitionKey = "Customer";

            id = Id;
            card = Card;
            name = Name;
            ex_date = Ex_date;
            balance = Balance;
        }

        public int card { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string ex_date { get; set; }
        public double balance { get; set; }
    }
}
