using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backoffice.Data;
using Backoffice.Models;

namespace Backoffice.Controllers
{
    public class EventModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Models.Include(e => e.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var eventModel = await _context.Models
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventModel == null)
            {
                return NotFound();
            }

            return View(eventModel);
        }

        // GET: EventModels/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Lookups, "Id", "Name");
            return View();
        }

        // POST: EventModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,DateFrom,DateTo,Cost,Status,CreatedAt,Deleted")] EventModel eventModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Lookups, "Id", "Id", eventModel.CategoryId);
            return View(eventModel);
        }

        // GET: EventModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var eventModel = await _context.Models.FindAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Lookups, "Id", "Id", eventModel.CategoryId);
            return View(eventModel);
        }

        // POST: EventModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,DateFrom,DateTo,Cost,Status,CreatedAt,Deleted")] EventModel eventModel)
        {
            if (id != eventModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventModelExists(eventModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Lookups, "Id", "Id", eventModel.CategoryId);
            return View(eventModel);
        }

        // GET: EventModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var eventModel = await _context.Models
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventModel == null)
            {
                return NotFound();
            }

            return View(eventModel);
        }

        // POST: EventModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Models == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Models'  is null.");
            }
            var eventModel = await _context.Models.FindAsync(id);
            if (eventModel != null)
            {
                _context.Models.Remove(eventModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventModelExists(int id)
        {
          return (_context.Models?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
