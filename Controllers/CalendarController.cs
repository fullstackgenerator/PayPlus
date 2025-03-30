using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PayPlus.Data;
using PayPlus.Models;
using System.Globalization;

namespace PayPlus.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetInvoiceEvents()
        {
            var invoices = _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.Services)
                .ToList();

            var events = invoices.Select(i => new
            {
                title = $"{i.Partner?.Name}",
                start = i.Date.ToString("yyyy-MM-dd"),
                allDay = true,
                extendedProps = new
                {
                    services = string.Join(", ", i.Services.Select(s => s.ServiceName)),
                    total = i.TotalPrice.ToString("C")
                }
            }).ToList();

            return Json(events);
        }
    }
}