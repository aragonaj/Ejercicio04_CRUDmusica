using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio03.Models;
using Microsoft.Data.SqlClient;

namespace Ejercicio03.Controllers
{
    public class AlbumesController : Controller
    {
        private readonly GrupoBContext _context;

        public AlbumesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Albumes
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Nombre"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["Fecha"] = sortOrder == "Fecha" ? "Fecha_desc" : "Fecha";
            ViewData["GenerosId"] = sortOrder == "GenerosId" ? "GenerosId_desc" : "GenerosId";
            ViewData["GruposId"] = sortOrder == "GruposId" ? "GruposId_desc" : "GruposId";
            var albumes = from album in _context.Albumes
                           select album;
            switch (sortOrder)
            {
                case "Nombre":
                    albumes = albumes.OrderByDescending(album => album.Nombre);
                    break;
                case "Fecha":
                    albumes = albumes.OrderBy(album => album.Fecha);
                    break;
                case "Fecha_desc":
                    albumes = albumes.OrderByDescending(album => album.Fecha);
                    break;
                case "GenerosId":
                    albumes = albumes.OrderBy(album => album.GenerosId);
                    break;
                case "GenerosId_desc":
                    albumes = albumes.OrderByDescending(album => album.GenerosId);
                    break;
                case "GruposId":
                    albumes = albumes.OrderBy(album => album.GruposId);
                    break;
                case "GruposId_desc":
                    albumes = albumes.OrderByDescending(album => album.GruposId);
                    break;
                default:
                    albumes = albumes.OrderBy(album => album.Nombre);
                    break;
            }
            return View(await albumes.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.Albumes.Include(a => a.Generos).Include(a => a.Grupos);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.Albumes
                .Include(a => a.Generos)
                .Include(a => a.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albume == null)
            {
                return NotFound();
            }

            return View(albume);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Id"); //"Canciones"
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id");
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id");
            return View();
        }

        // POST: Albumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GenerosId,GruposId,Fecha")] Albume albume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(albume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre", albume.GenerosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Id", albume.GruposId); //"Canciones"
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", albume.GenerosId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.Albumes.FindAsync(id);
            if (albume == null)
            {
                return NotFound();
            }
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre", albume.GenerosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Id", albume.GruposId); //"Canciones"
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", albume.GenerosId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", albume.GruposId);
            return View(albume);
        }

        // POST: Albumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GenerosId,GruposId,Fecha")] Albume albume)
        {
            if (id != albume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(albume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumeExists(albume.Id))
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
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre", albume.GenerosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Id", albume.GruposId); //"Canciones"
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", albume.GenerosId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.Albumes
                .Include(a => a.Generos)
                .Include(a => a.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albume == null)
            {
                return NotFound();
            }

            return View(albume);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albume = await _context.Albumes.FindAsync(id);
            if (albume != null)
            {
                _context.Albumes.Remove(albume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumeExists(int id)
        {
            return _context.Albumes.Any(e => e.Id == id);
        }
    }
}
