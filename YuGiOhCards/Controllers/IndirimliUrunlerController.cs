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
    public class IndirimliUrunlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IndirimliUrunlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IndirimliUrunler
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IndirimliUrunler.Include(i => i.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IndirimliUrunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirimliUrunler = await _context.IndirimliUrunler
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indirimliUrunler == null)
            {
                return NotFound();
            }

            return View(indirimliUrunler);
        }

        // GET: IndirimliUrunler/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id");
            return View();
        }

        // POST: IndirimliUrunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunId,Oran,Baslangic,Bitis,DigerKampanya")] IndirimliUrunler indirimliUrunler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indirimliUrunler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", indirimliUrunler.UrunId);
            return View(indirimliUrunler);
        }

        // GET: IndirimliUrunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirimliUrunler = await _context.IndirimliUrunler.FindAsync(id);
            if (indirimliUrunler == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", indirimliUrunler.UrunId);
            return View(indirimliUrunler);
        }

        // POST: IndirimliUrunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunId,Oran,Baslangic,Bitis,DigerKampanya")] IndirimliUrunler indirimliUrunler)
        {
            if (id != indirimliUrunler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indirimliUrunler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndirimliUrunlerExists(indirimliUrunler.Id))
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
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", indirimliUrunler.UrunId);
            return View(indirimliUrunler);
        }

        // GET: IndirimliUrunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirimliUrunler = await _context.IndirimliUrunler
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indirimliUrunler == null)
            {
                return NotFound();
            }

            return View(indirimliUrunler);
        }

        // POST: IndirimliUrunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var indirimliUrunler = await _context.IndirimliUrunler.FindAsync(id);
            _context.IndirimliUrunler.Remove(indirimliUrunler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndirimliUrunlerExists(int id)
        {
            return _context.IndirimliUrunler.Any(e => e.Id == id);
        }
    }
}
