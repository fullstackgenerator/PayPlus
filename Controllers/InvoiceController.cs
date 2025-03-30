using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;
using QuestPDF.Fluent;

namespace PayPlus.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index(string searchInvoices)
        {
            var query = _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.Services)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchInvoices))
            {
                string searchTerm = searchInvoices.ToLower();
                query = query.Where(i => 
                    (i.Partner != null && i.Partner.Name.ToLower().Contains(searchTerm)) ||
                    i.Services.Any(s => s.ServiceName.ToLower().Contains(searchTerm)) ||
                    i.TotalPrice.ToString().Contains(searchInvoices) ||
                    i.Date.ToString().Contains(searchInvoices));
            }
    
            var invoices = await query.ToListAsync();
            return View(invoices);
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.Services) // Include services
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Id");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PartnerId,OfferDate,TotalPrice")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Id", invoice.PartnerId);
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            ViewBag.PartnerId = new SelectList(_context.Partners, "Id", "Name", invoice.PartnerId);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PartnerId,OfferDate,TotalPrice")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Id", invoice.PartnerId);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateFromOffer(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.Partner)
                .Include(o => o.Services) // Make sure to include services
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
            {
                return NotFound();
            }

            // Create invoice from offer
            var invoice = new Invoice
            {
                PartnerId = offer.PartnerId,
                Partner = offer.Partner,
                Date = offer.Date,
                TotalPrice = offer.TotalPrice
            };

            // Add the services to the invoice
            foreach (var service in offer.Services)
            {
                invoice.Services.Add(service);
            }

            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportToPdf(int id)
        {
            var invoice = await _context.Invoice
                .Include(o => o.Partner) // Include Partner
                .Include(o => o.Services) // Include Services
                .FirstOrDefaultAsync(t => t.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            var document = GeneratePdf(invoice);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Invoice_{id}.pdf");
        }

        // Generate PDF document
        private Document GeneratePdf(Invoice invoice)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Invoice number #{invoice.Id}").Bold().FontSize(16);
                        col.Item().PaddingVertical(10);

                        // Partner information
                        col.Item().Text($"Partner: {invoice.Partner?.Name ?? "Not specified"}");

                        // Services list
                        col.Item().Text("Services:");
                        foreach (var service in invoice.Services)
                        {
                            col.Item().Text($"{service.ServiceName}: {service.Price:C}");

                        }

                        col.Item().PaddingVertical(10);

                        // Dates and totals
                        col.Item().Text($"Invoice date: {invoice.Date:dd.MM.yyyy}");
                        col.Item().Text($"Total price: {invoice.TotalPrice:C}").Bold();
                    });
                });
            });
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }
    }
}