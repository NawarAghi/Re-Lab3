using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Customer
    {
        public Customer(int card, string name, string ex_date, double balance)
        {
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