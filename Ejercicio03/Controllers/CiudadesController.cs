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
    public class CiudadesController : Controller
    {
        private readonly GrupoBContext _context;

        public CiudadesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Ciudades
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Nombre"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["Paises"] = sortOrder == "Paises" ? "Paises_desc" : "Paises";
            var ciudades = from ciudad in _context.Ciudades.Include(c => c.Paises)
                           select ciudad;
            switch (sortOrder)
            {
                case "Nombre":
                    ciudades = ciudades.OrderByDescending(ciudad => ciudad.Nombre);
                    break;
                case "Paises":
                    ciudades = ciudades.OrderBy(ciudad => ciudad.Paises);
                    break;
                case "Paises_desc":
                    ciudades = ciudades.OrderByDescending(ciudad => ciudad.Paises);
                    break;
                default:
                    ciudades = ciudades.OrderBy(ciudad => ciudad.Nombre);
                    break;
            }
            //var grupoBContext = _context.Ciudades.Include(c => c.Paises);
            //return View(await grupoBContext.ToListAsync());
            //return View(await ciudades.ToListAsync());
            return View(await ciudades.AsNoTracking().ToListAsync());
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudade = await _context.Ciudades
                .Include(c => c.Paises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudade == null)
            {
                return NotFound();
            }

            return View(ciudade);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            // para que en vez de mostrar el id, muestre el nombre del país
            //ViewData["idTabla"] = new SelectList(_context.NombreTabla,
            //  "campoQueSeQuiereSustituir", "campoQueSustituye");
            ViewData["PaisesId"] = new SelectList(_context.Paises,
                "Id", "Nombre");

            //ViewData["PaisesId"] = new SelectList(_context.Paises, "Id", "Id");
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PaisesId")] Ciudade ciudade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaisesId"] = new SelectList(_context.Paises,
                "Id", "Nombre", ciudade.PaisesId);
            //ViewData["PaisesId"] = new SelectList(_context.Paises, "Id", "Id", ciudade.PaisesId);
            return View(ciudade);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudade = await _context.Ciudades.FindAsync(id);
            if (ciudade == null)
            {
                return NotFound();
            }
            ViewData["PaisesId"] = new SelectList(_context.Paises,
                "Id", "Nombre", ciudade.PaisesId);
            //ViewData["PaisesId"] = new SelectList(_context.Paises, "Id", "Id", ciudade.PaisesId);
            return View(ciudade);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PaisesId")] Ciudade ciudade)
        {
            if (id != ciudade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadeExists(ciudade.Id))
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
            ViewData["PaisesId"] = new SelectList(_context.Paises,
                "Id", "Nombre", ciudade.PaisesId);
            //ViewData["PaisesId"] = new SelectList(_context.Paises, "Id", "Id", ciudade.PaisesId);
            return View(ciudade);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudade = await _context.Ciudades
                .Include(c => c.Paises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudade == null)
            {
                return NotFound();
            }

            return View(ciudade);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudade = await _context.Ciudades.FindAsync(id);
            if (ciudade != null)
            {
                _context.Ciudades.Remove(ciudade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadeExists(int id)
        {
            return _context.Ciudades.Any(e => e.Id == id);
        }
    }
}
