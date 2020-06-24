using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Passenger
    {
        public Passenger(string name, string dep, string arr)
        {
            Name = name;
            Dep = dep;
            Arr = arr;
        }

        public string Name { get; set; }
        public string Dep { get; set; }
        public string Arr { get; set; }
    }
}