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
    public class SepetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SepetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sepet
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sepet.Include(s => s.ApplicationUser).Include(s => s.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sepet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.ApplicationUser)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // GET: Sepet/Create
        public IActionResult Create()
        {
            ViewData["MusteriId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id");
            return View();
        }

        // POST: Sepet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunId,MusteriId,Miktar,Fiyat,SiparisOk")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MusteriId"] = new SelectList(_context.ApplicationUser, "Id", "Id", sepet.MusteriId);
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", sepet.UrunId);
            return View(sepet);
        }

        // GET: Sepet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet.FindAsync(id);
            if (sepet == null)
            {
                return NotFound();
            }
            ViewData["MusteriId"] = new SelectList(_context.ApplicationUser, "Id", "Id", sepet.MusteriId);
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", sepet.UrunId);
            return View(sepet);
        }

        // POST: Sepet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunId,MusteriId,Miktar,Fiyat,SiparisOk")] Sepet sepet)
        {
            if (id != sepet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetExists(sepet.Id))
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
            ViewData["MusteriId"] = new SelectList(_context.ApplicationUser, "Id", "Id", sepet.MusteriId);
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Id", sepet.UrunId);
            return View(sepet);
        }

        // GET: Sepet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.ApplicationUser)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // POST: Sepet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepet = await _context.Sepet.FindAsync(id);
            _context.Sepet.Remove(sepet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetExists(int id)
        {
            return _context.Sepet.Any(e => e.Id == id);
        }
        public class sepeteekle
        {
            public int UrunId { get; set; }

            public int Adet { get; set; }
            public string UrunAd { get; set; }
            public double UrunFiyat { get; set; }
            public string UrunFoto { get; set; }
            public string UrunKategori { get; set; }

            public int KategoriId { get; set; }


        }
    }
}
