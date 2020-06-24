using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Classes
{
    public class Airline
    {
        public Airline(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}