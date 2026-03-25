using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TravelQuotation.Domain.Entities;
using TravelQuotation.Domain.Interfaces;

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

                        // ✅ Extract City
                        var cityMatch = Regex.Match(body, @"City\s*[:=\-]\s*(.+)", RegexOptions.IgnoreCase);
                        string city = cityMatch.Success ? cityMatch.Groups[1].Value.Trim() : "";

                        // ✅ Extract Sender Email
                        var from = message.From.Mailboxes.FirstOrDefault();
                        string senderEmail = from?.Address ?? "";
                        // TODO: Parse body into TravelRequest
                        var request = new TravelRequest
                        {
                            // Example mapping (customize based on your email format)
                            //CustomerName = "From Email",
                            Email = senderEmail,
                            City = city

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
            var fromEmail = "n.manikandan1902@gmail.com";
            var password = "weir zemn flvb qaka"; // NOT your normal password

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = "Please find attached your travel quotation.";
                message.IsBodyHtml = false;

                // Attach PDF
                using (var stream = new MemoryStream(pdf))
                {
                    var attachment = new Attachment(stream, "Quotation.pdf", "application/pdf");
                    message.Attachments.Add(attachment);

                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(fromEmail, password);
                        smtp.EnableSsl = true;

                        await smtp.SendMailAsync(message);
                    }
                }
            }
        }
    }
}
