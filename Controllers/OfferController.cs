using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;

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
                .FirstOrDefaultAsync(m => m.Id == id);

            if (offer == null || offer.Partner == null)
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

            // Populate ViewBag.PartnerId with partner names
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

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.Id == id);
        }
    }
}