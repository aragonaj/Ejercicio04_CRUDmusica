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
        public async Task<IActionResult> Index(string sortOrder)
        {
            //ViewData["NombreCompleto"] = String.IsNullOrEmpty(sortOrder) ? "NombreCompleto" : "";
            //ViewData["FechaNacimiento"] = sortOrder == "FechaNacimiento" ? "FechaNacimiento_desc" : "FechaNacimiento";
            //ViewData["Identificacion"] = sortOrder == "Identificacion" ? "Identificacion_desc" : "Identificacion";
            //ViewData["Mail"] = sortOrder == "Mail" ? "Mail_desc" : "Mail";
            //ViewData["Telefono"] = sortOrder == "Telefono" ? "Telefono_desc" : "Telefono";
            //ViewData["Ciudades"] = sortOrder == "Ciudades" ? "Ciudades_desc" : "Ciudades";
            //var representantes = from representante in _context.Representantes.Include(r => r.Ciudades)
            //                     select representante;
            //switch (sortOrder)
            //{
            //    case "NombreCompleto":
            //        representantes = representantes.OrderByDescending(representante => representante.NombreCompleto);
            //        break;
            //    case "FechaNacimiento":
            //        representantes = representantes.OrderBy(representante => representante.FechaNacimiento);
            //        break;
            //    case "FechaNacimiento_desc":
            //        representantes = representantes.OrderByDescending(representante => representante.FechaNacimiento);
            //        break;
            //    case "Identificacion":
            //        representantes = representantes.OrderBy(representante => representante.Identificacion);
            //        break;
            //    case "Identificacion_desc":
            //        representantes = representantes.OrderByDescending(representante => representante.Identificacion);
            //        break;
            //    case "Mail":
            //        representantes = representantes.OrderBy(representante => representante.Mail);
            //        break;
            //    case "Mail_desc":
            //        representantes = representantes.OrderByDescending(representante => representante.Mail);
            //        break; 
            //    case "Telefono":
            //        representantes = representantes.OrderBy(representante => representante.Telefono);
            //        break;
            //    case "Telefono_desc":
            //        representantes = representantes.OrderByDescending(representante => representante.Telefono);
            //        break;
            //    case "Ciudades":
            //        representantes = representantes.OrderBy(representante => representante.Ciudades);
            //        break;
            //    case "Ciudades_desc":
            //        representantes = representantes.OrderByDescending(representante => representante.Ciudades);
            //        break;
            //    default:
            //        representantes = representantes.OrderBy(concierto => concierto.NombreCompleto);
            //        break;
            //}
            //return View(await representantes.AsNoTracking().ToListAsync());
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", representante.CiudadesId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", representante.CiudadesId);
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", representante.CiudadesId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", representante.CiudadesId);
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades,
                "Id", "Nombre", representante.CiudadesId);
            //ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Id", representante.CiudadesId);
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
