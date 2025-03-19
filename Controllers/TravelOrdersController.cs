using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayPlus.Data;
using PayPlus.Models;

namespace PayPlus.Views_TravelOrder
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool TravelOrderExists(int id)
        {
            return _context.TravelOrder.Any(e => e.order_id == id);
        }
    }
}
