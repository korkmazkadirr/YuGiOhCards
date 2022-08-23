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
    public class ToptanUrunlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToptanUrunlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToptanUrunler
        public IActionResult toptanlistele()
        {
            var UrunListesi = (from u in _context.Urun
                               join k in _context.Kategori on u.KategoriId equals k.Id
                               join f in _context.Foto on u.Id equals f.UrunId
                               select new UrunTransfer
                               {
                                   UrunId = u.Id,
                                   UrunAd = u.Ad,
                                   UrunFiyat = u.Fiyat,
                                   UrunFoto = f.ResimAd,
                                   UrunKategori = k.Ad,




                               }).ToList();
            return View(UrunListesi.Where(k => k.UrunKategori == "Toptan Ürünler"));

        }

        // GET: ToptanUrunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: ToptanUrunler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToptanUrunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Durum")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: ToptanUrunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: ToptanUrunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Durum")] Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.Id))
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
            return View(kategori);
        }

        // GET: ToptanUrunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: ToptanUrunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategori = await _context.Kategori.FindAsync(id);
            _context.Kategori.Remove(kategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategori.Any(e => e.Id == id);
        }
        public class UrunTransfer
        {
            public int UrunId { get; set; }
            public string UrunAd { get; set; }
            public double UrunFiyat { get; set; }
            public string UrunFoto { get; set; }
            public string UrunKategori { get; set; }

            public int KategoriId { get; set; }

        }
    }
}
