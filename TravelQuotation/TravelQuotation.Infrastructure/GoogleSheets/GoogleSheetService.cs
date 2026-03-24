using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;
using TravelQuotation.Domain.Interfaces;

namespace TravelQuotation.Infrastructure.GoogleSheets
{
    public class GoogleSheetService : IGoogleSheetService
    {
        public async Task<List<Hotel>> GetHotels(string city)
        {
            return new List<Hotel>();
            // Call Google Sheets API
            // Filter by city
        }
    }
}
