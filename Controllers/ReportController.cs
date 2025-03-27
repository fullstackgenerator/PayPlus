using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;

namespace PayPlus.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
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
                    Invoices = new List<Invoice>() // Empty list for error case
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
    }
}