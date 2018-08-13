using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kings.Data;
using Kings.Models;

namespace Kings.Controllers
{
    public class KingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kings
        public async Task<IActionResult> Index()
        {
            return View(await _context.King.ToListAsync());
        }

        // GET: Kings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var king = await _context.King
                .SingleOrDefaultAsync(m => m.ID == id);
            if (king == null)
            {
                return NotFound();
            }

            return View(king);
        }

        // GET: Kings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] King king)
        {
            if (ModelState.IsValid)
            {
                _context.Add(king);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(king);
        }

        // GET: Kings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var king = await _context.King.SingleOrDefaultAsync(m => m.ID == id);
            if (king == null)
            {
                return NotFound();
            }
            return View(king);
        }

        // POST: Kings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] King king)
        {
            if (id != king.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(king);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KingExists(king.ID))
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
            return View(king);
        }

        // GET: Kings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var king = await _context.King
                .SingleOrDefaultAsync(m => m.ID == id);
            if (king == null)
            {
                return NotFound();
            }

            return View(king);
        }

        // POST: Kings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var king = await _context.King.SingleOrDefaultAsync(m => m.ID == id);
            _context.King.Remove(king);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KingExists(int id)
        {
            return _context.King.Any(e => e.ID == id);
        }
    }
}
