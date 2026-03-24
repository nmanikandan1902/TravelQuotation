using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelQuotation.Domain.Entities
{

    public class TravelRequest
    {
        public string Email { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class Hotel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public decimal CostPerNight { get; set; }
    }
}
