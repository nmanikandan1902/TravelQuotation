using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelQuotation.Domain.Interfaces;

namespace TravelQuotation.Application.UseCases
{
    public class ProcessTravelQuotation
    {
        private readonly IEmailService _emailService;
        private readonly IGoogleSheetService _sheetService;
        private readonly IPdfService _pdfService;

        public ProcessTravelQuotation(
            IEmailService emailService,
            IGoogleSheetService sheetService,
            IPdfService pdfService)
        {
            _emailService = emailService;
            _sheetService = sheetService;
            _pdfService = pdfService;
        }

        public async Task ExecuteAsync()
        {
            var emails = await _emailService.ReadUnreadEmailsAsync();

            foreach (var request in emails)
            {
                var hotels = await _sheetService.GetHotels(request.City);

                var pdf = _pdfService.GenerateQuotation(request, hotels);

                await _emailService.SendEmailAsync(
                    request.Email,
                    "Your Travel Quotation",
                    pdf
                );
            }
        }
    }
}
