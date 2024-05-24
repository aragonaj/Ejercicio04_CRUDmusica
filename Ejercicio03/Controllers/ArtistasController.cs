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
    public class ArtistasController : Controller
    {
        private readonly GrupoBContext _context;

        public ArtistasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["Nombre"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["FechaDeNacimiento"] = sortOrder == "FechaDeNacimiento" ? "FechaDeNacimiento_desc" : "FechaDeNacimiento";
            ViewData["CiudadesId"] = sortOrder == "CiudadesId" ? "CiudadesId_desc" : "CiudadesId";
            ViewData["GenerosId"] = sortOrder == "GenerosId" ? "GenerosId_desc" : "GenerosId";
            ViewData["GruposId"] = sortOrder == "GruposId" ? "GruposId_desc" : "GruposId";
            var artistas = from artista in _context.Artistas.Include(a => a.Ciudades).Include(a => a.Generos).Include(a => a.Grupos)
                           select artista;

            switch (sortOrder)
            {
                case "Nombre":
                    artistas = artistas.OrderByDescending(artista => artista.Nombre);
                    break;
                case "FechaDeNacimiento":
                    artistas = artistas.OrderBy(artista => artista.FechaDeNacimiento);
                    break;
                case "FechaDeNacimiento_desc":
                    artistas = artistas.OrderByDescending(artista => artista.FechaDeNacimiento);
                    break;
                case "CiudadesId":
                    artistas = artistas.OrderBy(artista => artista.CiudadesId);
                    break;
                case "CiudadesId_desc":
                    artistas = artistas.OrderByDescending(artista => artista.CiudadesId);
                    break;
                case "GenerosId":
                    artistas = artistas.OrderBy(artista => artista.GenerosId);
                    break;
                case "GenerosId_desc":
                    artistas = artistas.OrderByDescending(artista => artista.GenerosId);
                    break;
                case "GruposId":
                    artistas = artistas.OrderBy(artista => artista.GruposId);
                    break;
                case "GruposId_desc":
                    artistas = artistas.OrderByDescending(artista => artista.GruposId);
                    break;
                default:
                    artistas = artistas.OrderBy(artista => artista.Nombre);
                    break;
            }
            
            return View(await artistas.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.Artistas.Include(a => a.Ciudades).Include(a => a.Generos).Include(a => a.Grupos);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .Include(a => a.Ciudades)
                .Include(a => a.Generos)
                .Include(a => a.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre");
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id");
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id");
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id");
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre", artista.GruposId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", artista.CiudadesId);
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", artista.GenerosId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre", artista.GruposId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", artista.CiudadesId);
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", artista.GenerosId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", artista.GruposId);
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artista artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistaExists(artista.Id))
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre", artista.GruposId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", artista.CiudadesId);
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", artista.GenerosId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .Include(a => a.Ciudades)
                .Include(a => a.Generos)
                .Include(a => a.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            if (artista != null)
            {
                _context.Artistas.Remove(artista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistaExists(int id)
        {
            return _context.Artistas.Any(e => e.Id == id);
        }
    }
}
