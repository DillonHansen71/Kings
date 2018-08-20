using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kings.Data;
using Kings.Models.ManageViewModels;

namespace Kings.Controllers
{
    public class KingdomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KingdomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Calculate resources
        public async Task<RedirectToActionResult> UpdateResourcesAsync(int id)
        {
            //var id = 1;
            //Need to target the Kindom of the currently logged on player
            var kingdom = await _context.Kingdom.SingleOrDefaultAsync(m => m.ID == id);

            //Get current time
            var UtcNow = DateTime.UtcNow;

            //Find the number of seconds between the LastUpdated and now
            var SecondsSenseLastUpdate = ( UtcNow - kingdom.LastUpdated).TotalSeconds;

            // update gold value
            kingdom.Gold = Convert.ToInt32(kingdom.Gold + (kingdom.Citizen * SecondsSenseLastUpdate));

            
            //Update kingdom.lastupdated
            kingdom.LastUpdated = UtcNow;

            //save changes to DB
            _context.Update(kingdom);
            await _context.SaveChangesAsync();

            ViewBag.Goldfield = (id.ToString() + "gold");

            //DateTime LastUpdated =
            return RedirectToAction("Index", kingdom);
        }


        // GET: Kingdoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kingdom.ToListAsync());
        }

        // GET: Kingdoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kingdom = await _context.Kingdom
                .SingleOrDefaultAsync(m => m.ID == id);
            if (kingdom == null)
            {
                return NotFound();
            }

            return View(kingdom);
        }

        // GET: Kingdoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kingdoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,KingID,Citizen,Gold,LastUpdated")] Kingdom kingdom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kingdom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kingdom);
        }

        // GET: Kingdoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kingdom = await _context.Kingdom.SingleOrDefaultAsync(m => m.ID == id);
            if (kingdom == null)
            {
                return NotFound();
            }
            return View(kingdom);
        }

        // POST: Kingdoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,KingID,Citizen,Gold,LastUpdated")] Kingdom kingdom)
        {
            if (id != kingdom.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kingdom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KingdomExists(kingdom.ID))
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
            return View(kingdom);
        }

        // GET: Kingdoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kingdom = await _context.Kingdom
                .SingleOrDefaultAsync(m => m.ID == id);
            if (kingdom == null)
            {
                return NotFound();
            }

            return View(kingdom);
        }

        // POST: Kingdoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kingdom = await _context.Kingdom.SingleOrDefaultAsync(m => m.ID == id);
            _context.Kingdom.Remove(kingdom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KingdomExists(int id)
        {
            return _context.Kingdom.Any(e => e.ID == id);
        }
    }
}
