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
    public class VideoClipsController : Controller
    {
        private readonly GrupoBContext _context;

        public VideoClipsController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: VideoClips
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.VideoClips.Include(v => v.Canciones);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: VideoClips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClip = await _context.VideoClips
                .Include(v => v.Canciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClip == null)
            {
                return NotFound();
            }

            return View(videoClip);
        }

        // GET: VideoClips/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Canciones,
                "Id", "Titulo");
            //ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id");
            return View();
        }

        // POST: VideoClips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CancionesId,Fecha")] VideoClip videoClip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoClip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id", videoClip.CancionesId);
            return View(videoClip);
        }

        // GET: VideoClips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClip = await _context.VideoClips.FindAsync(id);
            if (videoClip == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id", videoClip.CancionesId);
            return View(videoClip);
        }

        // POST: VideoClips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancionesId,Fecha")] VideoClip videoClip)
        {
            if (id != videoClip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoClip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoClipExists(videoClip.Id))
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
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id", videoClip.CancionesId);
            return View(videoClip);
        }

        // GET: VideoClips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClip = await _context.VideoClips
                .Include(v => v.Canciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClip == null)
            {
                return NotFound();
            }

            return View(videoClip);
        }

        // POST: VideoClips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoClip = await _context.VideoClips.FindAsync(id);
            if (videoClip != null)
            {
                _context.VideoClips.Remove(videoClip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoClipExists(int id)
        {
            return _context.VideoClips.Any(e => e.Id == id);
        }
    }
}
