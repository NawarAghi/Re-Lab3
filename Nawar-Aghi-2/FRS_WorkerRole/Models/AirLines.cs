using Microsoft.WindowsAzure.Storage.Table;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole
{
    public class Airlines : TableEntity
    {
        public Airlines() { }
        public Airlines(int id, string code, string name)
        {
            this.PartitionKey = "Airlines";
            Id = id;
            Code = code;
            Name = name;
        }

        public int Id {get;set;}
        public string Code {get;set;}
        public string Name{get;set;}
    }
}