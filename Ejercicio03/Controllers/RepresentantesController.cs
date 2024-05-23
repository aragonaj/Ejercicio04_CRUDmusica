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
    public class RepresentantesController : Controller
    {
        private readonly GrupoBContext _context;

        public RepresentantesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Representantes
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.Representantes.Include(r => r.Ciudades);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Representantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representante = await _context.Representantes
                .Include(r => r.Ciudades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (representante == null)
            {
                return NotFound();
            }

            return View(representante);
        }

        // GET: Representantes/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre");
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id");
            return View();
        }

        // POST: Representantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,Mail,Telefono,CiudadesId")] Representante representante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(representante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", representante.CiudadesId);
            return View(representante);
        }

        // GET: Representantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representante = await _context.Representantes.FindAsync(id);
            if (representante == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", representante.CiudadesId);
            return View(representante);
        }

        // POST: Representantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,Mail,Telefono,CiudadesId")] Representante representante)
        {
            if (id != representante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(representante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepresentanteExists(representante.Id))
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", representante.CiudadesId);
            return View(representante);
        }

        // GET: Representantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representante = await _context.Representantes
                .Include(r => r.Ciudades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (representante == null)
            {
                return NotFound();
            }

            return View(representante);
        }

        // POST: Representantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var representante = await _context.Representantes.FindAsync(id);
            if (representante != null)
            {
                _context.Representantes.Remove(representante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepresentanteExists(int id)
        {
            return _context.Representantes.Any(e => e.Id == id);
        }
    }
}
