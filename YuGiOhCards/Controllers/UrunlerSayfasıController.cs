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
    public class UrunlerSayfasıController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunlerSayfasıController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Listele()
        {
            var Urunlistesi = (from u in _context.Urun
                            join k in _context.Kategori on u.KategoriId equals k.Id
                            join f in _context.Foto on u.Id equals f.UrunId
                            select new UrunTransfer
                            {
                                UrunId = u.Id,
                                UrunAd = u.Ad,
                                UrunFiyat = u.Fiyat,
                                UrunFoto = f.ResimAd,
                                UrunKategori = k.Ad,
                                UrunAciklama = u.Aciklama
                                



                            }).ToList();
            return View(Urunlistesi);
        }

        // GET: UrunlerSayfası
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Urun.Include(u => u.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UrunlerSayfası/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: UrunlerSayfası/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id");
            return View();
        }

        // POST: UrunlerSayfası/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Fiyat,Miktar,Aciklama,UretimYeri,KategoriId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", urun.KategoriId);
            return View(urun);
        }

        // GET: UrunlerSayfası/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", urun.KategoriId);
            return View(urun);
        }

        // POST: UrunlerSayfası/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Fiyat,Miktar,Aciklama,UretimYeri,KategoriId")] Urun urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", urun.KategoriId);
            return View(urun);
        }

        // GET: UrunlerSayfası/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: UrunlerSayfası/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urun = await _context.Urun.FindAsync(id);
            _context.Urun.Remove(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.Urun.Any(e => e.Id == id);
        }
        public class UrunTransfer
        {
            public int UrunId { get; set; }
            public string UrunAd { get; set; }
            public string UrunAciklama { get; set; }
            public double UrunFiyat { get; set; }
            public string UrunFoto { get; set; }
            public string UrunKategori { get; set; }
            
            public int KategoriId { get; set; }

        }
    }
}

