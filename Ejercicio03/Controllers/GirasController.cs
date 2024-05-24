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
    public class GirasController : Controller
    {
        private readonly GrupoBContext _context;

        public GirasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Giras
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Nombre"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["FechaInicio"] = sortOrder == "FechaInicio" ? "FechaInicio_desc" : "FechaInicio";
            ViewData["FechaFin"] = sortOrder == "FechaFin" ? "FechaFin_desc" : "FechaFin";
            ViewData["Grupos"] = sortOrder == "Grupos" ? "Grupos_desc" : "Grupos";
            var giras = from gira in _context.Giras
                             select gira;
            switch (sortOrder)
            {
                case "Nombre":
                    giras = giras.OrderByDescending(gira => gira.Nombre);
                    break;
                case "FechaInicio":
                    giras = giras.OrderBy(gira => gira.FechaInicio);
                    break;
                case "FechaInicio_desc":
                    giras = giras.OrderByDescending(gira => gira.FechaInicio);
                    break;
                case "FechaFin":
                    giras = giras.OrderBy(gira => gira.FechaFin);
                    break;
                case "FechaFin_desc":
                    giras = giras.OrderByDescending(gira => gira.FechaFin);
                    break;
                case "Grupos":
                    giras = giras.OrderBy(gira => gira.Grupos);
                    break;
                case "Grupos_desc":
                    giras = giras.OrderByDescending(gira => gira.Grupos);
                    break;
                default:
                    giras = giras.OrderBy(gira => gira.Nombre);
                    break;
            }
            return View(await giras.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.Giras.Include(g => g.Grupos);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _context.Giras
                .Include(g => g.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gira == null)
            {
                return NotFound();
            }

            return View(gira);
        }

        // GET: Giras/Create
        public IActionResult Create()
        {
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre");
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id");
            return View();
        }

        // POST: Giras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GruposId,FechaInicio,FechaFin")] Gira gira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre", gira.GruposId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", gira.GruposId);
            return View(gira);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _context.Giras.FindAsync(id);
            if (gira == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre", gira.GruposId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", gira.GruposId);
            return View(gira);
        }

        // POST: Giras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GruposId,FechaInicio,FechaFin")] Gira gira)
        {
            if (id != gira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiraExists(gira.Id))
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
            ViewData["GruposId"] = new SelectList(_context.Grupos,
                "Id", "Nombre", gira.GruposId);
            //ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Id", gira.GruposId);
            return View(gira);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _context.Giras
                .Include(g => g.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gira == null)
            {
                return NotFound();
            }

            return View(gira);
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gira = await _context.Giras.FindAsync(id);
            if (gira != null)
            {
                _context.Giras.Remove(gira);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiraExists(int id)
        {
            return _context.Giras.Any(e => e.Id == id);
        }
    }
}
