using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole.Models
{
    public class Transaction : TableEntity
    {
        public Transaction() { }
        public Transaction(string date, int card, double amount)
        {
            this.PartitionKey = "Transaction";

            Date = date;
            Card = card;
            Amount = amount;
        }

        public string Date { get; set; }
        public int Card { get; set; }
        public double Amount { get; set; }
    }
}
