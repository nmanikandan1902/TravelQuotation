using TravelQuotation.Application.UseCases;
using TravelQuotation.Domain.Interfaces;
using TravelQuotation.Infrastructure.Email;
using TravelQuotation.Infrastructure.GoogleSheets;
using TravelQuotation.Infrastructure.Pdf;
using TravelQuotation.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IGoogleSheetService, GoogleSheetService>();
builder.Services.AddScoped<IPdfService, PdfService>();

builder.Services.AddScoped<ProcessTravelQuotation>();
var host = builder.Build();
host.Run();
