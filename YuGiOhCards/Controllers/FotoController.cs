using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YuGiOhCards.Data;
using YuGiOhCards.Models;

namespace YuGiOhCards.Controllers
{
    public class FotoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public FotoController(ApplicationDbContext context, IWebHostEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        // GET: Foto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Foto.Include(f => f.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Foto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Foto
                .Include(f => f.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foto == null)
            {
                return NotFound();
            }

            return View(foto);
        }

        // GET: Foto/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Ad");
            return View();
        }

        // POST: Foto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResimAd,UrunId")] Foto foto)
        {
            if (ModelState.IsValid)
            {
                //WebHostEnvironment

                string webRootPath = _hostingEnviroment.WebRootPath;
                var files = HttpContext.Request.Form.Files;


                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images/UrunFoto");
                var extension = Path.GetExtension(files[0].FileName);//yüklenen resim dosyasının uzantısı

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                foto.ResimAd = @"/Images/UrunFoto" + "/" + fileName + extension;

                //***************
                _context.Add(foto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Ad", foto.UrunId);
            return View(foto);
        }

        // GET: Foto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Foto.FindAsync(id);
            if (foto == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Ad", foto.UrunId);
            return View(foto);
        }

        // POST: Foto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResimAd,UrunId")] Foto foto)
        {
            if (id != foto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotoExists(foto.Id))
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
            ViewData["UrunId"] = new SelectList(_context.Urun, "Id", "Ad", foto.UrunId);
            return View(foto);
        }

        // GET: Foto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Foto
                .Include(f => f.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foto == null)
            {
                return NotFound();
            }

            return View(foto);
        }

        // POST: Foto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foto = await _context.Foto.FindAsync(id);
            _context.Foto.Remove(foto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotoExists(int id)
        {
            return _context.Foto.Any(e => e.Id == id);
        }
    }
}
