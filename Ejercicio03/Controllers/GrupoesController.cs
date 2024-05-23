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
    public class GrupoesController : Controller
    {
        private readonly GrupoBContext _context;

        public GrupoesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Grupoes
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.Grupos.Include(g => g.Ciudades).Include(g => g.Generos).Include(g => g.Representantes);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Grupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.Ciudades)
                .Include(g => g.Generos)
                .Include(g => g.Representantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupoes/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(_context.Generos,
                "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes,
                "Id", "NombreCompleto");
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id");
            //ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id");
            //ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "Id");
            return View();
        }

        // POST: Grupoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Grupo1,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", grupo.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", grupo.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "Id", grupo.RepresentantesId);
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", grupo.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", grupo.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "Id", grupo.RepresentantesId);
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Grupo1,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.Id))
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", grupo.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Id", grupo.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "Id", grupo.RepresentantesId);
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.Ciudades)
                .Include(g => g.Generos)
                .Include(g => g.Representantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupos.Remove(grupo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
