using MailKit;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;
using TravelQuotation.Domain.Interfaces;
using MailKit.Net.Imap;

namespace TravelQuotation.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public async Task<List<TravelRequest>> ReadUnreadEmailsAsync()
        {
            //return new List<TravelRequest>();
            // Connect IMAP
            // Filter subject: "Travel quotation"
            // Parse email body → TravelRequest
            var requests = new List<TravelRequest>();

            using (var client = new ImapClient())
            {
                await client.ConnectAsync("imap.gmail.com", 993, true);

                // Login
                await client.AuthenticateAsync("n.manikandan1902@gmail.com", "weir zemn flvb qaka");

                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadWrite);

                // Get unread emails
                var uids = await inbox.SearchAsync(SearchQuery.NotSeen);

                foreach (var uid in uids)
                {
                    var message = await inbox.GetMessageAsync(uid);

                    // ✅ Filter by subject
                    if (message.Subject != null && message.Subject.Contains("Travel Quotation"))
                    {
                        var body = message.TextBody;

                        // TODO: Parse body into TravelRequest
                        var request = new TravelRequest
                        {
                            // Example mapping (customize based on your email format)
                            //CustomerName = "From Email",
                            Email = body
                        };

                        requests.Add(request);

                        // Mark as read
                        await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true);
                    }
                }

                await client.DisconnectAsync(true);
            }

            return requests;
        }

        public async Task SendEmailAsync(string to, string subject, byte[] pdf)
        {
            // SMTP send with attachment
        }
    }
}
