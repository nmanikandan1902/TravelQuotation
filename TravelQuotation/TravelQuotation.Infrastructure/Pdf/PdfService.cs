using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;
using TravelQuotation.Domain.Interfaces;

namespace TravelQuotation.Infrastructure.Pdf
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateQuotation(TravelRequest request, List<Hotel> hotels)
        {
            byte[] bytes = null;
            return bytes;
            // Build clean PDF layout
        }
    }
}
