using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;
using QuestPDF.Fluent;

namespace PayPlus.Controllers
{
    public class TravelOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TravelOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelOrder.ToListAsync());
        }

        // GET: TravelOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelOrder = await _context.TravelOrder
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (travelOrder == null)
            {
                return NotFound();
            }

            return View(travelOrder);
        }

        // GET: TravelOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("order_date,date_start,date_end,location_start,location_end,full_name_driver,car_brand_and_model,car_type,trip_reason,full_name_organizer")] TravelOrder travelOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelOrder);
        }

        // GET: TravelOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelOrder = await _context.TravelOrder.FindAsync(id);
            if (travelOrder == null)
            {
                return NotFound();
            }
            return View(travelOrder);
        }

        // POST: TravelOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("order_date,date_start,date_end,location_start,location_end,full_name_driver,car_brand_and_model,car_type,trip_reason,full_name_organizer")] TravelOrder travelOrder)
        {
            if (id != travelOrder.order_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelOrderExists(travelOrder.order_id))
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
            return View(travelOrder);
        }

        // GET: TravelOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelOrder = await _context.TravelOrder
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (travelOrder == null)
            {
                return NotFound();
            }

            return View(travelOrder);
        }

        // POST: TravelOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelOrder = await _context.TravelOrder.FindAsync(id);
            if (travelOrder != null)
            {
                _context.TravelOrder.Remove(travelOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Export a travel order to PDF
        public async Task<IActionResult> ExportToPdf(int id)
        {
            var travelOrder = await _context.TravelOrder.FirstOrDefaultAsync(t => t.order_id == id);
            if (travelOrder == null)
            {
                return NotFound();
            }

            var document = GeneratePdf(travelOrder);
            var pdfBytes = document.GeneratePdf();

            // Return the PDF file as a response
            return File(pdfBytes, "application/pdf", $"Travel_order_{id}.pdf");
        }

        // Generate PDF document
        private Document GeneratePdf(TravelOrder travelOrder)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Potni nalog #{travelOrder.order_id}");
                        col.Item().Text($"Datum dokumenta: {travelOrder.order_date:dd.MM.yyyy}");
                        col.Item().Text($"Začetek: {travelOrder.date_start:dd.MM.yyyy}");
                        col.Item().Text($"Zaključek: {travelOrder.date_end:dd.MM.yyyy}");
                        col.Item().Text($"Začetna lokacija: {travelOrder.location_start}");
                        col.Item().Text($"Lokacija: {travelOrder.location_end}");
                        col.Item().Text($"Voznik: {travelOrder.full_name_driver}");
                        col.Item().Text($"Znamka in model: {travelOrder.car_brand_and_model}");
                        col.Item().Text($"Vrsta vozila: {travelOrder.car_type}");
                        col.Item().Text($"Namen: {travelOrder.trip_reason}");
                        col.Item().Text($"Odobril/a: {travelOrder.full_name_organizer}");
                    });
                });
            });
        }
        
        private bool TravelOrderExists(int id)
        {
            return _context.TravelOrder.Any(e => e.order_id == id);
        }
    }
}
