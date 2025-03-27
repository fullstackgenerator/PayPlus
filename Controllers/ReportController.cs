using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PayPlus.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // GET: Reports/InvoiceReport
        public IActionResult InvoiceReport(DateTime? startDate, DateTime? endDate, int? selectedPartnerId)
        {
            // Set default dates if not provided
            if (!startDate.HasValue)
                startDate = DateTime.Now.AddMonths(-1);
            if (!endDate.HasValue)
                endDate = DateTime.Now;

            // Validate date range
            if (endDate < startDate)
            {
                ModelState.AddModelError("EndDate", "End date must be after start date");
                var errorReport = new Report
                {
                    ReportDate = DateTime.Now,
                    StartDate = startDate.Value,
                    EndDate = endDate.Value,
                    Partners = _context.Partners.ToList(),
                    Services = _context.Services.ToList(),
                    SelectedPartnerId = selectedPartnerId,
                    Invoices = new List<Invoice>()
                };
                return View(errorReport);
            }

            // Query invoices with filtering
            var query = _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.Services)
                .Where(i => i.Date >= startDate && i.Date <= endDate);

            // Apply partner filter if selected
            if (selectedPartnerId.HasValue && selectedPartnerId.Value > 0)
            {
                query = query.Where(i => i.PartnerId == selectedPartnerId.Value);
            }

            var invoices = query
                .OrderByDescending(i => i.Date)
                .ToList();

            // Calculate statistics
            var totalAmount = invoices.Sum(i => i.TotalPrice);
            var serviceCount = invoices.Sum(i => i.Services.Count);
            var partnerCount = invoices.Select(i => i.PartnerId).Distinct().Count();

            // Create report model
            var report = new Report
            {
                ReportDate = DateTime.Now,
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                Partners = _context.Partners.OrderBy(p => p.Name).ToList(),
                Services = _context.Services.ToList(),
                Invoices = invoices,
                TotalAmount = totalAmount,
                SelectedPartnerId = selectedPartnerId,
                ServiceCount = serviceCount,
                PartnerCount = partnerCount
            };

            return View(report);
        }

        public async Task<IActionResult> ExportReportToPdf(DateTime? startDate, DateTime? endDate,
            int? selectedPartnerId)
        {
            if (!startDate.HasValue)
                startDate = DateTime.Now.AddMonths(-1);
            if (!endDate.HasValue)
                endDate = DateTime.Now;

            var query = _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.Services)
                .Where(i => i.Date >= startDate && i.Date <= endDate);

            if (selectedPartnerId.HasValue && selectedPartnerId.Value > 0)
            {
                query = query.Where(i => i.PartnerId == selectedPartnerId.Value);
            }

            var invoices = await query
                .OrderByDescending(i => i.Date)
                .ToListAsync();

            var partners = await _context.Partners.OrderBy(p => p.Name).ToListAsync();
            var selectedPartner = selectedPartnerId.HasValue
                ? partners.FirstOrDefault(p => p.Id == selectedPartnerId.Value)
                : null;

            var document = GenerateReportPdf(
                invoices,
                startDate.Value,
                endDate.Value,
                selectedPartner,
                invoices.Sum(i => i.TotalPrice),
                invoices.Sum(i => i.Services.Count),
                invoices.Select(i => i.PartnerId).Distinct().Count()
            );

            var pdfBytes = document.GeneratePdf();
            var fileName = $"InvoiceReport_{startDate:yyyyMMdd}_to_{endDate:yyyyMMdd}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }

        private Document GenerateReportPdf(
            List<Invoice> invoices,
            DateTime startDate,
            DateTime endDate,
            Partner selectedPartner,
            decimal totalAmount,
            int serviceCount,
            int partnerCount)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .PaddingBottom(10)
                        .Text("Invoice Report")
                        .Bold().FontSize(16).AlignCenter();

                    page.Content()
                        .PaddingVertical(10)
                        .Column(col =>
                        {
                            // Report summary
                            col.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text($"Date Range: {startDate:dd.MM.yyyy} to {endDate:dd.MM.yyyy}");
                            });

                            if (selectedPartner != null)
                            {
                                col.Item().Text($"Partner: {selectedPartner.Name}");
                            }

                            col.Item().PaddingBottom(10);

                            // Invoices table
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(25);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(2);
                                });
                                
                                table.Header(header =>
                                {
                                    header.Cell().Text("#").Bold();
                                    header.Cell().Text("Date").Bold();
                                    header.Cell().Text("Partner").Bold();
                                    header.Cell().Text("Services").Bold();
                                    header.Cell().Text("Amount").Bold().AlignRight();

                                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                                });
                                
                                foreach (var (invoice, index) in invoices.Select((v, i) => (v, i)))
                                {
                                    table.Cell().Text(index + 1);
                                    table.Cell().Text(invoice.Date.ToString("dd.MM.yyyy"));
                                    table.Cell().Text(invoice.Partner?.Name ?? "-");
                                    table.Cell().Text(string.Join(", ", invoice.Services.Select(s => s.ServiceName)));
                                    table.Cell().Text(invoice.TotalPrice.ToString("C")).AlignRight();

                                    table.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2);
                                }
                                
                                table.Footer(footer =>
                                {
                                    footer.Cell().ColumnSpan(3).Text("Total:").Bold();
                                    footer.Cell().ColumnSpan(2).Text(totalAmount.ToString("C")).Bold().AlignRight();
                                });
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });
        }
    }
}