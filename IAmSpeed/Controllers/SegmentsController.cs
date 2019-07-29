using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAmSpeed.Data;
using IAmSpeed.Models;

namespace IAmSpeed.Controllers
{
    public class SegmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SegmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Segments
        public async Task<IActionResult> Index(string gameName)
        {
            ViewData["gameName"] = gameName; 
            if (!String.IsNullOrEmpty(gameName))
            {
            ApiHelper.InitializeClient();
            var gameData = await TestClass.LoadData(gameName);
                return View(gameData);
            }
            else
            {
                return View();
            }
            //var applicationDbContext = _context.Segments.Include(s => s.Game).Include(s => s.User);
            //return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: Segments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Include(s => s.User)
                .Include(s => s.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null)
            {
                return NotFound();
            }

            return View(segment);
        }

        // GET: Segments/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Segments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,VideoLink,Notes,PBTime,RNG,Category,UserId,GameId")] Segment segment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(segment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", segment.GameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", segment.UserId);
            return View(segment);
        }

        // GET: Segments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments.FindAsync(id);
            if (segment == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", segment.GameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", segment.UserId);
            return View(segment);
        }

        // POST: Segments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,VideoLink,Notes,PBTime,RNG,Category,UserId,GameId")] Segment segment)
        {
            if (id != segment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(segment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegmentExists(segment.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", segment.GameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", segment.UserId);
            return View(segment);
        }

        // GET: Segments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Include(s => s.Game)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null)
            {
                return NotFound();
            }

            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var segment = await _context.Segments.FindAsync(id);
            _context.Segments.Remove(segment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.Id == id);
        }
    }
}
