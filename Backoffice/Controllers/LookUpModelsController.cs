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
    public class LookUpModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LookUpModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LookUpModels
        public async Task<IActionResult> Index()
        {
              return _context.Lookups != null ? 
                          View(await _context.Lookups.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lookups'  is null.");
        }

        // GET: LookUpModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lookups == null)
            {
                return NotFound();
            }

            var lookUpModel = await _context.Lookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookUpModel == null)
            {
                return NotFound();
            }

            return View(lookUpModel);
        }

        // GET: LookUpModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LookUpModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Order")] LookUpModel lookUpModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lookUpModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lookUpModel);
        }

        // GET: LookUpModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lookups == null)
            {
                return NotFound();
            }

            var lookUpModel = await _context.Lookups.FindAsync(id);
            if (lookUpModel == null)
            {
                return NotFound();
            }
            return View(lookUpModel);
        }

        // POST: LookUpModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Order")] LookUpModel lookUpModel)
        {
            if (id != lookUpModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lookUpModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LookUpModelExists(lookUpModel.Id))
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
            return View(lookUpModel);
        }

        // GET: LookUpModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lookups == null)
            {
                return NotFound();
            }

            var lookUpModel = await _context.Lookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookUpModel == null)
            {
                return NotFound();
            }

            return View(lookUpModel);
        }

        // POST: LookUpModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lookups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lookups'  is null.");
            }
            var lookUpModel = await _context.Lookups.FindAsync(id);
            if (lookUpModel != null)
            {

                _context.Lookups.Remove(lookUpModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LookUpModelExists(int id)
        {
          return (_context.Lookups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
