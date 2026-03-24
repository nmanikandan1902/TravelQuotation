using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;

namespace TravelQuotation.Domain.Interfaces
{

    public interface IEmailService
    {
        Task<List<TravelRequest>> ReadUnreadEmailsAsync();
        Task SendEmailAsync(string to, string subject, byte[] pdf);
    }
}
