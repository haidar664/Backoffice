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
    public class EventsToGuidesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsToGuidesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventsToGuides
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventsToGuides.Include(e => e.Event).Include(e => e.Guide);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventsToGuides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventsToGuides == null)
            {
                return NotFound();
            }

            var eventsToGuides = await _context.EventsToGuides
                .Include(e => e.Event)
                .Include(e => e.Guide)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventsToGuides == null)
            {
                return NotFound();
            }

            return View(eventsToGuides);
        }

        // GET: EventsToGuides/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name");
            ViewData["GuideId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: EventsToGuides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuideId,EventId")] EventsToGuides eventsToGuides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventsToGuides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name", eventsToGuides.EventId);
            ViewData["GuideId"] = new SelectList(_context.Users, "Id", "Id", eventsToGuides.GuideId);
            return View(eventsToGuides);
        }

        // GET: EventsToGuides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventsToGuides == null)
            {
                return NotFound();
            }

            var eventsToGuides = await _context.EventsToGuides.FindAsync(id);
            if (eventsToGuides == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name", eventsToGuides.EventId);
            ViewData["GuideId"] = new SelectList(_context.Users, "Id", "Id", eventsToGuides.GuideId);
            return View(eventsToGuides);
        }

        // POST: EventsToGuides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("GuideId,EventId")] EventsToGuides eventsToGuides)
        {
            if (id != eventsToGuides.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsToGuides);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsToGuidesExists(eventsToGuides.EventId))
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
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name", eventsToGuides.EventId);
            ViewData["GuideId"] = new SelectList(_context.Users, "Id", "Id", eventsToGuides.GuideId);
            return View(eventsToGuides);
        }

        // GET: EventsToGuides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventsToGuides == null)
            {
                return NotFound();
            }

            var eventsToGuides = await _context.EventsToGuides
                .Include(e => e.Event)
                .Include(e => e.Guide)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventsToGuides == null)
            {
                return NotFound();
            }

            return View(eventsToGuides);
        }

        // POST: EventsToGuides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.EventsToGuides == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventsToGuides'  is null.");
            }
            var eventsToGuides = await _context.EventsToGuides.FindAsync(id);
            if (eventsToGuides != null)
            {
                _context.EventsToGuides.Remove(eventsToGuides);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsToGuidesExists(int? id)
        {
          return (_context.EventsToGuides?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
