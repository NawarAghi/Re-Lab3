using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Transaction
    {
        public Transaction(string date, int card, double amount)
        {
            Date = date;
            Card = card;
            Amount = amount;
        }

        public string Date { get; set; }
        public int Card { get; set; }
        public double Amount { get; set; }
    }
}