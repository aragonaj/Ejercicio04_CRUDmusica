using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio03.Models;

namespace Ejercicio03.Controllers
{
    public class CancionesController : Controller
    {
        private readonly GrupoBContext _context;

        public CancionesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Canciones
        public async Task<IActionResult> Index(string sortOrder)
        {
            //ViewData["Titulo"] = String.IsNullOrEmpty(sortOrder) ? "Titulo" : "";
            //ViewData["Duracion"] = sortOrder == "Duracion" ? "Duracion_desc" : "Duracion";
            //ViewData["Single"] = sortOrder == "Single" ? "Single_desc" : "Single";
            //ViewData["Albumes"] = sortOrder == "Albumes" ? "Albumes_desc" : "Albumes";
            //var canciones = from cancion in _context.Canciones.Include(c => c.Albumes)
            //                select cancion;
            //switch (sortOrder)
            //{
            //    case "Titulo":
            //        canciones = canciones.OrderByDescending(cancion => cancion.Titulo);
            //        break;
            //    case "Duracion":
            //        canciones = canciones.OrderBy(cancion => cancion.Duracion);
            //        break;
            //    case "Duracion_desc":
            //        canciones = canciones.OrderByDescending(cancion => cancion.Duracion);
            //        break;
            //    case "Single":
            //        canciones = canciones.OrderBy(cancion => cancion.Single);
            //        break;
            //    case "Single_desc":
            //        canciones = canciones.OrderByDescending(cancion => cancion.Single);
            //        break;
            //    case "Albumes":
            //        canciones = canciones.OrderBy(cancion => cancion.Albumes);
            //        break;
            //    case "Albumes_desc":
            //        canciones = canciones.OrderByDescending(cancion => cancion.Albumes);
            //        break;
            //    default:
            //        canciones = canciones.OrderBy(cancion => cancion.Titulo);
            //        break;
            //}
            //return View(await canciones.AsNoTracking().ToListAsync());
            var grupoBContext = _context.Canciones.Include(c => c.Albumes);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones
                .Include(c => c.Albumes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // GET: Canciones/Create
        public IActionResult Create()
        {
            ViewData["AlbumesId"] = new SelectList(_context.Albumes,
                "Id", "Nombre");
            //ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Id");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracion,AlbumesId,Single")] Cancione cancione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(_context.Albumes,
                "Id", "Nombre", cancione.AlbumesId);
            //ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Id", cancione.AlbumesId);
            return View(cancione);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones.FindAsync(id);
            if (cancione == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(_context.Albumes,
                "Id", "Nombre", cancione.AlbumesId);
            //ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Id", cancione.AlbumesId);
            return View(cancione);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracion,AlbumesId,Single")] Cancione cancione)
        {
            if (id != cancione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancioneExists(cancione.Id))
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
            ViewData["AlbumesId"] = new SelectList(_context.Albumes,
                 "Id", "Nombre", cancione.AlbumesId);
            //ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Id", cancione.AlbumesId);
            return View(cancione);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones
                .Include(c => c.Albumes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancione = await _context.Canciones.FindAsync(id);
            if (cancione != null)
            {
                _context.Canciones.Remove(cancione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancioneExists(int id)
        {
            return _context.Canciones.Any(e => e.Id == id);
        }
    }
}
