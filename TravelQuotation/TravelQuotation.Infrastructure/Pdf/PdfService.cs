//using System.Reflection.Metadata;
using TravelQuotation.Domain.Entities;
using TravelQuotation.Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace TravelQuotation.Infrastructure.Pdf
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateQuotation(TravelRequest request, List<Hotel> hotels)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text("Travel Quotation")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        // Customer Info
                        col.Item().Text($"City: {request.City}");
                        col.Item().Text($"Start Date: {request.StartDate:dd MMM yyyy}");

                        col.Item().LineHorizontal(1);

                        // Hotel Table Header
                        col.Item().Text("Available Hotels")
                            .Bold()
                            .FontSize(14);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Name
                                columns.RelativeColumn(2); // City
                                columns.RelativeColumn(2); // Price
                            });

                            // Header
                            table.Header(header =>
                            {
                                header.Cell().Text("Hotel Name").Bold();
                                header.Cell().Text("City").Bold();
                                header.Cell().Text("Cost/Night").Bold();
                            });

                            // Data Rows
                            foreach (var hotel in hotels)
                            {
                                table.Cell().Text(hotel.Name);
                                table.Cell().Text(hotel.City);
                                table.Cell().Text($"₹ {hotel.CostPerNight}");
                            }
                        });

                        col.Item().LineHorizontal(1);

                        col.Item().Text("Thank you for choosing our service!")
                            .AlignCenter()
                            .Italic();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("dd MMM yyyy"));
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
