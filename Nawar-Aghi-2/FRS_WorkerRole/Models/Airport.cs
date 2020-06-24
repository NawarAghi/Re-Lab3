
using Microsoft.WindowsAzure.Storage.Table;

namespace FRS_WorkerRole
{
    public class Airport : TableEntity
    {
        public Airport() { }
        public Airport(int Id, string Code, string City, double Latitude, double Longitude)
        {
            this.PartitionKey = "Airport";

            id = Id;
            code = Code;
            city = City;
            latitude = Latitude;
            longitude = Longitude;
        }

        public string code { get; set; }
        public int id { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}