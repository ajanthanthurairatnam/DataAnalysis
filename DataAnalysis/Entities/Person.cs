using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Entities
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Attribute { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public Double Lat { get; set; }
        public Double Lon { get; set; }
    }
}
