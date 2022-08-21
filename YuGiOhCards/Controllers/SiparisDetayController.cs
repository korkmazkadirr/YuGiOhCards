using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YuGiOhCards.Data;
using YuGiOhCards.Models;

namespace YuGiOhCards.Controllers
{
    public class SiparisDetayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiparisDetayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiparisDetay
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SiparisDetay.Include(s => s.Siparis).Include(s => s.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SiparisDetay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparisDetay = await _context.SiparisDetay
                .Include(s => s.Siparis)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparisDetay == null)
            {
                return NotFound();
            }

            return View(siparisDetay);
        }

        // GET: SiparisDetay/Create
        public IActionResult Create()
        {
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id");
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id");
            return View();
        }

        // POST: SiparisDetay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiparisId,UrunId,Miktar,Fiyat")] SiparisDetay siparisDetay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siparisDetay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id", siparisDetay.SiparisId);
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", siparisDetay.UrunId);
            return View(siparisDetay);
        }

        // GET: SiparisDetay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparisDetay = await _context.SiparisDetay.FindAsync(id);
            if (siparisDetay == null)
            {
                return NotFound();
            }
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id", siparisDetay.SiparisId);
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", siparisDetay.UrunId);
            return View(siparisDetay);
        }

        // POST: SiparisDetay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiparisId,UrunId,Miktar,Fiyat")] SiparisDetay siparisDetay)
        {
            if (id != siparisDetay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparisDetay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisDetayExists(siparisDetay.Id))
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
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id", siparisDetay.SiparisId);
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", siparisDetay.UrunId);
            return View(siparisDetay);
        }

        // GET: SiparisDetay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparisDetay = await _context.SiparisDetay
                .Include(s => s.Siparis)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparisDetay == null)
            {
                return NotFound();
            }

            return View(siparisDetay);
        }

        // POST: SiparisDetay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siparisDetay = await _context.SiparisDetay.FindAsync(id);
            _context.SiparisDetay.Remove(siparisDetay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisDetayExists(int id)
        {
            return _context.SiparisDetay.Any(e => e.Id == id);
        }
    }
}
