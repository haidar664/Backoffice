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
    public class EventsToMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsToMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventsToMembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventsToMembers.Include(e => e.Event).Include(e => e.Guide);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventsToMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventsToMembers == null)
            {
                return NotFound();
            }

            var eventsToMembers = await _context.EventsToMembers
                .Include(e => e.Event)
                .Include(e => e.Guide)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventsToMembers == null)
            {
                return NotFound();
            }

            return View(eventsToMembers);
        }

        // GET: EventsToMembers/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name");
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: EventsToMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,EventId")] EventsToMembers eventsToMembers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventsToMembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name", eventsToMembers.EventId);
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id", eventsToMembers.MemberId);
            return View(eventsToMembers);
        }

        // GET: EventsToMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventsToMembers == null)
            {
                return NotFound();
            }

            var eventsToMembers = await _context.EventsToMembers.FindAsync(id);
            if (eventsToMembers == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name", eventsToMembers.EventId);
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id", eventsToMembers.MemberId);
            return View(eventsToMembers);
        }

        // POST: EventsToMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("MemberId,EventId")] EventsToMembers eventsToMembers)
        {
            if (id != eventsToMembers.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsToMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsToMembersExists(eventsToMembers.EventId))
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
            ViewData["EventId"] = new SelectList(_context.Models, "Id", "Name", eventsToMembers.EventId);
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id", eventsToMembers.MemberId);
            return View(eventsToMembers);
        }

        // GET: EventsToMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventsToMembers == null)
            {
                return NotFound();
            }

            var eventsToMembers = await _context.EventsToMembers
                .Include(e => e.Event)
                .Include(e => e.Guide)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventsToMembers == null)
            {
                return NotFound();
            }

            return View(eventsToMembers);
        }

        // POST: EventsToMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.EventsToMembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventsToMembers'  is null.");
            }
            var eventsToMembers = await _context.EventsToMembers.FindAsync(id);
            if (eventsToMembers != null)
            {
                _context.EventsToMembers.Remove(eventsToMembers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsToMembersExists(int? id)
        {
          return (_context.EventsToMembers?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
