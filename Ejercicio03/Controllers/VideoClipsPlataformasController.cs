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
    public class VideoClipsPlataformasController : Controller
    {
        private readonly GrupoBContext _context;

        public VideoClipsPlataformasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: VideoClipsPlataformas
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Plataformas"] = String.IsNullOrEmpty(sortOrder) ? "Plataforma" : "";
            ViewData["VideoClips"] = sortOrder == "VideoClips" ? "VideoClips_desc" : "VideoClips";
            var videoclipsPlataformas = from videoclipPlataformas in _context.VideoClipsPlataformas.Include(v => v.Plataformas).Include(v => v.VideoClips)
                                        select videoclipPlataformas;
            switch (sortOrder)
            {
                case "Fecha":
                    videoclipsPlataformas = videoclipsPlataformas.OrderByDescending(videoclipPlataformas => videoclipPlataformas.Plataformas);
                    break;
                case "VideoClips":
                    videoclipsPlataformas = videoclipsPlataformas.OrderBy(videoclipPlataformas => videoclipPlataformas.VideoClips);
                    break;
                case "VideoClips_desc":
                    videoclipsPlataformas = videoclipsPlataformas.OrderByDescending(videoclipPlataformas => videoclipPlataformas.VideoClips);
                    break;
                default:
                    videoclipsPlataformas = videoclipsPlataformas.OrderBy(videoclipPlataformas => videoclipPlataformas.Plataformas);
                    break;
            }
            return View(await videoclipsPlataformas.AsNoTracking().ToListAsync());
            //var grupoBContext = _context.VideoClipsPlataformas.Include(v => v.Plataformas).Include(v => v.VideoClips);
            //return View(await grupoBContext.ToListAsync());
        }

        // GET: VideoClipsPlataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataforma = await _context.VideoClipsPlataformas
                .Include(v => v.Plataformas)
                .Include(v => v.VideoClips)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClipsPlataforma == null)
            {
                return NotFound();
            }

            return View(videoClipsPlataforma);
        }

        // GET: VideoClipsPlataformas/Create
        public IActionResult Create()
        {
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas,
                "Id", "Nombre");
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips,
                "Id", "Id"); //"Canciones"
            //ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Id");
            //ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id");
            return View();
        }

        // POST: VideoClipsPlataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlataformasId,VideoClipsId,Url")] VideoClipsPlataforma videoClipsPlataforma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoClipsPlataforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas,
                "Id", "Nombre", videoClipsPlataforma.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips,
                "Id", "Id", videoClipsPlataforma.VideoClipsId); //"Canciones"
            //ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Id", videoClipsPlataforma.PlataformasId);
            //ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id", videoClipsPlataforma.VideoClipsId);
            return View(videoClipsPlataforma);
        }

        // GET: VideoClipsPlataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataforma = await _context.VideoClipsPlataformas.FindAsync(id);
            if (videoClipsPlataforma == null)
            {
                return NotFound();
            }
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas,
                "Id", "Nombre", videoClipsPlataforma.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips,
                "Id", "Id", videoClipsPlataforma.VideoClipsId); //"Canciones"
            //ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Id", videoClipsPlataforma.PlataformasId);
            //ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id", videoClipsPlataforma.VideoClipsId);
            return View(videoClipsPlataforma);
        }

        // POST: VideoClipsPlataformas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlataformasId,VideoClipsId,Url")] VideoClipsPlataforma videoClipsPlataforma)
        {
            if (id != videoClipsPlataforma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoClipsPlataforma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoClipsPlataformaExists(videoClipsPlataforma.Id))
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
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas,
                "Id", "Nombre", videoClipsPlataforma.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips,
                "Id", "Id", videoClipsPlataforma.VideoClipsId); //"Canciones"
            //ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Id", videoClipsPlataforma.PlataformasId);
            //ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id", videoClipsPlataforma.VideoClipsId);
            return View(videoClipsPlataforma);
        }

        // GET: VideoClipsPlataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataforma = await _context.VideoClipsPlataformas
                .Include(v => v.Plataformas)
                .Include(v => v.VideoClips)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClipsPlataforma == null)
            {
                return NotFound();
            }

            return View(videoClipsPlataforma);
        }

        // POST: VideoClipsPlataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoClipsPlataforma = await _context.VideoClipsPlataformas.FindAsync(id);
            if (videoClipsPlataforma != null)
            {
                _context.VideoClipsPlataformas.Remove(videoClipsPlataforma);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoClipsPlataformaExists(int id)
        {
            return _context.VideoClipsPlataformas.Any(e => e.Id == id);
        }
    }
}
