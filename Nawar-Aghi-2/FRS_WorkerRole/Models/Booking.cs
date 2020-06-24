using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_WorkerRole.Models
{
    public class Booking : TableEntity
    {
        public Booking(string pass, string name, string arr_date, int nmb_nights, double fee, int code)
        {
            this.PartitionKey = "Booking";

            Pass = pass;
            Name = name;
            Arr_date = arr_date;
            Nmb_nights = nmb_nights;
            Fee = fee;
            Code = code;
        }

        public string Pass { get; set; }
        public string Name { get; set; }
        public string Arr_date { get; set; }
        public int Nmb_nights { get; set; }
        public double Fee { get; set; }
        public int Code { get; set; }
    }
}
