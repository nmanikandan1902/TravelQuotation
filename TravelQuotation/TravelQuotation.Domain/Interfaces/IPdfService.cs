using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;

namespace TravelQuotation.Domain.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateQuotation(TravelRequest request, List<Hotel> hotels);
    }
}
