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
    public class ConciertoesController : Controller
    {
        private readonly GrupoBContext _context;

        public ConciertoesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Conciertoes
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Fecha"] = String.IsNullOrEmpty(sortOrder) ? "Fecha" : "";
            ViewData["Direccion"] = sortOrder == "Direccion" ? "Direccion_desc" : "Direccion";
            ViewData["Ciudades"] = sortOrder == "Ciudades" ? "Ciudades_desc" : "Ciudades";
            ViewData["Giras"] = sortOrder == "Giras" ? "Giras_desc" : "Giras";
            var conciertos = from concierto in _context.Conciertos.Include(c => c.Ciudades).Include(c => c.Giras)
                             select concierto;
            switch (sortOrder)
            {
                case "Fecha":
                    conciertos = conciertos.OrderByDescending(concierto => concierto.Fecha);
                    break;
                case "Direccion":
                    conciertos = conciertos.OrderBy(concierto => concierto.Direccion);
                    break;
                case "Direccion_desc":
                    conciertos = conciertos.OrderByDescending(concierto => concierto.Direccion);
                    break;
                case "Ciudades":
                    conciertos = conciertos.OrderBy(concierto => concierto.Ciudades);
                    break;
                case "Ciudades_desc":
                    conciertos = conciertos.OrderByDescending(concierto => concierto.Ciudades);
                    break;
                case "Giras":
                    conciertos = conciertos.OrderBy(concierto => concierto.Giras);
                    break;
                case "Giras_desc":
                    conciertos = conciertos.OrderByDescending(concierto => concierto.Giras);
                    break;
                default:
                    conciertos = conciertos.OrderBy(concierto => concierto.Fecha);
                    break;
            }
            return View(await conciertos.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.Conciertos.Include(c => c.Ciudades).Include(c => c.Giras);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: Conciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .Include(c => c.Ciudades)
                .Include(c => c.Giras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // GET: Conciertoes/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre");
            ViewData["GirasId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre");
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id");
            //ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Id");
            return View();
        }

        // POST: Conciertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Concierto concierto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concierto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", concierto.CiudadesId);
            ViewData["GirasId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", concierto.GirasId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", concierto.CiudadesId);
            //ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Id", concierto.GirasId);
            return View(concierto);
        }

        // GET: Conciertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos.FindAsync(id);
            if (concierto == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", concierto.CiudadesId);
            ViewData["GirasId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", concierto.GirasId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", concierto.CiudadesId);
            //ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Id", concierto.GirasId);
            return View(concierto);
        }

        // POST: Conciertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Concierto concierto)
        {
            if (id != concierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concierto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertoExists(concierto.Id))
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
                "Id", "Nombre", concierto.CiudadesId);
            ViewData["GirasId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", concierto.GirasId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", concierto.CiudadesId);
            //ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Id", concierto.GirasId);
            return View(concierto);
        }

        // GET: Conciertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .Include(c => c.Ciudades)
                .Include(c => c.Giras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // POST: Conciertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = await _context.Conciertos.FindAsync(id);
            if (concierto != null)
            {
                _context.Conciertos.Remove(concierto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertoExists(int id)
        {
            return _context.Conciertos.Any(e => e.Id == id);
        }
    }
}
