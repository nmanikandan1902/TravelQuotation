using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;
using TravelQuotation.Domain.Interfaces;

namespace TravelQuotation.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public async Task<List<TravelRequest>> ReadUnreadEmailsAsync()
        {
            return new List<TravelRequest>();
            // Connect IMAP
            // Filter subject: "Travel quotation"
            // Parse email body → TravelRequest
        }

        public async Task SendEmailAsync(string to, string subject, byte[] pdf)
        {
            // SMTP send with attachment
        }
    }
}
