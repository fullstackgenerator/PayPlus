using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;
using QuestPDF.Fluent;

namespace PayPlus.Controllers
{
    public class OfferController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfferController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Offer
        public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers
                .Include(o => o.Partner)
                .Include(o => o.Services)
                .ToListAsync();
            return View(offers);
        }

        // GET: Offer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Partner)
                .Include(o => o.Services)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offer/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Partners = await _context.Partners
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();

            ViewBag.Services = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.ServiceName} - {s.Price:C}"
                })
                .ToListAsync();

            return View();
        }

        // POST: Offer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerId,TotalPrice")] Offer offer, List<int> ServiceIds)
        {
            if (ModelState.IsValid)
            {
                var selectedServices = await _context.Services
                    .Where(s => ServiceIds.Contains(s.Id))
                    .ToListAsync();

                offer.Services.AddRange(selectedServices);
                offer.TotalPrice = selectedServices.Sum(s => s.Price);

                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Partners = _context.Partners
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

            ViewBag.Services = _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.ServiceName} - {s.Price:C}"
                })
                .ToList();

            return View(offer);
        }

        // GET: Offer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            
            ViewBag.PartnerId = new SelectList(_context.Partners, "Id", "Name", offer.PartnerId);
            return View(offer);
        }

        // POST: Offer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PartnerId,OfferDate,TotalPrice")] Offer offer)
        {
            if (id != offer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Id", offer.PartnerId);
            return View(offer);
        }

        // GET: Offer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Partner)
                .Include(o => o.Services)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }
        

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Export a travel order to PDF
        public async Task<IActionResult> ExportToPdf(int id)
        {
            var offer = await _context.Offer
                .Include(o => o.Partner)  // Include Partner
                .Include(o => o.Services) // Include Services
                .FirstOrDefaultAsync(t => t.Id == id);
    
            if (offer == null)
            {
                return NotFound();
            }

            var document = GeneratePdf(offer);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Offer_{id}.pdf");
        }

        // Generate PDF document
        private Document GeneratePdf(Offer offer)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Offer ID #{offer.Id}").Bold().FontSize(16);
                        col.Item().PaddingVertical(10);
                
                        // Partner information
                        col.Item().Text($"Partner: {offer.Partner?.Name ?? "Not specified"}");
                
                        // Services list
                        col.Item().Text("Services:");
                        foreach (var service in offer.Services)
                        {
                            col.Item().Text($"- {service.ServiceName}: {service.Price:C}");
                        }
                
                        col.Item().PaddingVertical(10);
                
                        // Dates and totals
                        col.Item().Text($"Offer date: {offer.Date:dd.MM.yyyy}");
                        col.Item().Text($"Total price: {offer.TotalPrice:C}").Bold();
                    });
                });
            });
        }
        
        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.Id == id);
        }
        
        public async Task<IActionResult> ToInvoice(int id)
        {
            return RedirectToAction("CreateFromOffer", "Invoice", new { id = id });
        }
    }
}