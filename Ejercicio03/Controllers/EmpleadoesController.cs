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
    public class EmpleadoesController : Controller
    {
        private readonly GrupoBContext _context;

        public EmpleadoesController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NombreCompleto"] = String.IsNullOrEmpty(sortOrder) ? "NombreCompleto" : "";
            ViewData["Roles"] = sortOrder == "Roles" ? "Roles_desc" : "Roles";
            var empleados = from empleado in _context.Empleados
                             select empleado;
            switch (sortOrder)
            {
                case "NombreCompleto":
                    empleados = empleados.OrderByDescending(empleado => empleado.NombreCompleto);
                    break;
                case "Roles":
                    empleados = empleados.OrderByDescending(empleado => empleado.Roles);
                    break;
                case "Roles_desc":
                    empleados = empleados.OrderByDescending(empleado => empleado.Roles);
                    break;
                default:
                    empleados = empleados.OrderBy(concierto => concierto.NombreCompleto);
                    break;
            }
            return View(await empleados.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.Empleados.Include(e => e.Roles);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            ViewData["RolesId"] = new SelectList(_context.Roles,
                "Id", "Descripcion");
            //ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,RolesId")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolesId"] = new SelectList(_context.Roles,
                "Id", "Descripcion", empleado.RolesId);
            //ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Id", empleado.RolesId);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["RolesId"] = new SelectList(_context.Roles,
                "Id", "Descripcion", empleado.RolesId);
            //ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Id", empleado.RolesId);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,RolesId")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            ViewData["RolesId"] = new SelectList(_context.Roles,
                "Id", "Descripcion", empleado.RolesId);
            //ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Id", empleado.RolesId);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
