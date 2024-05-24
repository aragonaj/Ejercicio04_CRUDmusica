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
    public class FuncionesController : Controller
    {
        private readonly GrupoBContext _context;

        public FuncionesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Funciones
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            var funciones = from funcion in _context.Funciones
                           select funcion;
            switch (sortOrder)
            {
                case "Nombre":
                    funciones = funciones.OrderByDescending(funcion => funcion.Nombre);
                    break;
                default:
                    funciones = funciones.OrderBy(funcion => funcion.Nombre);
                    break;
            }
            return View(await funciones.ToListAsync());
            //return View(await _context.Funciones.ToListAsync());
        }

        // GET: Funciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcione = await _context.Funciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcione == null)
            {
                return NotFound();
            }

            return View(funcione);
        }

        // GET: Funciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Funcione funcione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcione);
        }

        // GET: Funciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcione = await _context.Funciones.FindAsync(id);
            if (funcione == null)
            {
                return NotFound();
            }
            return View(funcione);
        }

        // POST: Funciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Funcione funcione)
        {
            if (id != funcione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncioneExists(funcione.Id))
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
            return View(funcione);
        }

        // GET: Funciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcione = await _context.Funciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcione == null)
            {
                return NotFound();
            }

            return View(funcione);
        }

        // POST: Funciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcione = await _context.Funciones.FindAsync(id);
            if (funcione != null)
            {
                _context.Funciones.Remove(funcione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncioneExists(int id)
        {
            return _context.Funciones.Any(e => e.Id == id);
        }
    }
}
