using Microsoft.WindowsAzure.Storage.Table;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole
{
    public class Passenger
    {
        public Passenger(string name, string dep, string arr)
        {
            Name = name;
            Dep = dep;
            Arr = arr;
        }

        public string Name {get;set;}
        public string Dep {get;set;}
        public string Arr {get;set;}
    }
}
