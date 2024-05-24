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
    public class FuncionesArtistasController : Controller
    {
        private readonly GrupoBContext _context;

        public FuncionesArtistasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: FuncionesArtistas
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Artistas"] = String.IsNullOrEmpty(sortOrder) ? "Artistas" : "";
            ViewData["Funciones"] = sortOrder == "Funciones" ? "Funciones_desc" : "Funciones";
            var funcionesArtistas = from funcion in _context.FuncionesArtistas.Include(f => f.Artistas).Include(f => f.Funciones)
                                    select funcion;
            switch (sortOrder)
            {
                case "Artistas":
                    funcionesArtistas = funcionesArtistas.OrderByDescending(concierto => concierto.Artistas);
                    break;
                case "Funciones":
                    funcionesArtistas = funcionesArtistas.OrderBy(concierto => concierto.Artistas);
                    break;
                case "Funciones_desc":
                    funcionesArtistas = funcionesArtistas.OrderByDescending(concierto => concierto.Artistas);
                    break;
                default:
                    funcionesArtistas = funcionesArtistas.OrderBy(concierto => concierto.Artistas);
                    break;
            }
            return View(await funcionesArtistas.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.FuncionesArtistas.Include(f => f.Artistas).Include(f => f.Funciones);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: FuncionesArtistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtista = await _context.FuncionesArtistas
                .Include(f => f.Artistas)
                .Include(f => f.Funciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionesArtista == null)
            {
                return NotFound();
            }

            return View(funcionesArtista);
        }

        // GET: FuncionesArtistas/Create
        public IActionResult Create()
        {
            ViewData["ArtistasId"] = new SelectList(_context.Artistas,
                "Id", "Nombre");
            ViewData["FuncionesId"] = new SelectList(_context.Funciones,
                "Id", "Nombre");
            //ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Id");
            //ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Id");
            return View();
        }

        // POST: FuncionesArtistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuncionesId,ArtistasId")] FuncionesArtista funcionesArtista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionesArtista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas,
                "Id", "Nombre", funcionesArtista.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(_context.Funciones,
                "Id", "Nombre", funcionesArtista.FuncionesId);
            //ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Id", funcionesArtista.ArtistasId);
            //ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Id", funcionesArtista.FuncionesId);
            return View(funcionesArtista);
        }

        // GET: FuncionesArtistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtista = await _context.FuncionesArtistas.FindAsync(id);
            if (funcionesArtista == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas,
                "Id", "Nombre", funcionesArtista.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(_context.Funciones,
                "Id", "Nombre", funcionesArtista.FuncionesId);
            //ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Id", funcionesArtista.ArtistasId);
            //ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Id", funcionesArtista.FuncionesId);
            return View(funcionesArtista);
        }

        // POST: FuncionesArtistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuncionesId,ArtistasId")] FuncionesArtista funcionesArtista)
        {
            if (id != funcionesArtista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionesArtista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionesArtistaExists(funcionesArtista.Id))
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
            ViewData["ArtistasId"] = new SelectList(_context.Artistas,
                "Id", "Nombre", funcionesArtista.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(_context.Funciones,
                "Id", "Nombre", funcionesArtista.FuncionesId);
            //ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Id", funcionesArtista.ArtistasId);
            //ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Id", funcionesArtista.FuncionesId);
            return View(funcionesArtista);
        }

        // GET: FuncionesArtistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtista = await _context.FuncionesArtistas
                .Include(f => f.Artistas)
                .Include(f => f.Funciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionesArtista == null)
            {
                return NotFound();
            }

            return View(funcionesArtista);
        }

        // POST: FuncionesArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionesArtista = await _context.FuncionesArtistas.FindAsync(id);
            if (funcionesArtista != null)
            {
                _context.FuncionesArtistas.Remove(funcionesArtista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionesArtistaExists(int id)
        {
            return _context.FuncionesArtistas.Any(e => e.Id == id);
        }
    }
}
