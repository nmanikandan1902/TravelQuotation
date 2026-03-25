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
        private readonly HttpClient _httpClient;

        public GoogleSheetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Hotel>> GetHotels(string city)
        {
            var url = "https://docs.google.com/spreadsheets/d/1o35AE3U7oJZxb_QESpQWUIaLaNNdW-9rfzL-hmUEZ3s/export?format=csv&gid=0";

            var csvData = await _httpClient.GetStringAsync(url);

            var hotels = new List<Hotel>();
            var lines = csvData.Split('\n');

            for (int i = 1; i < lines.Length; i++) // skip header
            {
                var columns = lines[i].Split(',');

                if (columns.Length < 3)
                    continue;

                var hotel = new Hotel
                {
                    HotelId = int.Parse(columns[0].Trim()),
                    City = columns[1].Trim(),
                    Name = columns[2].Trim(),
                    CostPerNight = decimal.Parse(columns[4].Trim())
                };

                if (!string.IsNullOrEmpty(city) &&
                    hotel.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                {
                    hotels.Add(hotel);
                }
            }

            return hotels;
        }
    }
}
