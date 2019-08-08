using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAmSpeed.Data;
using IAmSpeed.Models;
using Microsoft.AspNetCore.Identity;
using IAmSpeed.Models.GameViewModels;

namespace IAmSpeed.Controllers
{
    public class MySegmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Task GetCurrentUserAsyc { get; private set; }

        public MySegmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //Gets games for current user to display, this will sort segments user can view by games. 
        public async Task<IActionResult> GameSelect()
        {
            var currentUser = await GetCurrentUserAsync();

            var game = _context.Games
                .Where(g => g.UserId == currentUser.Id);

            return View(game);
        }

        // GET: MySegment
        public async Task<IActionResult> Index(int id)
        {
            GameSegmentViewModel gameSegment = new GameSegmentViewModel(); 

            var segmentsByGame = _context.Segments
                .Where(s => s.GameId == id).ToList();

            gameSegment.segmentsList = segmentsByGame;

            var currentGame = await _context.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if(currentGame != null)
            {
            gameSegment.Game = currentGame; 
            }
            //var applicationDbContext = _context.Segments.Include(s => s.Game).Include(s => s.User);
            return View(gameSegment);
        }

        // GET: MySegment/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: MySegment/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: MySegment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrginId,Name,Description,VideoLink,Notes,PBTime,RNG,Category,UserId,GameId")] Segment segment)
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

        // GET: MySegment/Edit/5
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

        // POST: MySegment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrginId,Name,Description,VideoLink,Notes,PBTime,RNG,Category,UserId,GameId")] Segment segment)
        {
            if (id != segment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var segmentExtra = _context.Segments
                    //    .FirstOrDefault(s => s.Id == id);

                    //segment.GameId = segmentExtra.GameId;
                    //segment.UserId = segmentExtra.UserId;
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
                //return RedirectToAction(nameof(Index));
                return Redirect($"http://localhost:5000/MySegment/Index/{segment.GameId}");
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", segment.GameId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", segment.UserId);
            return View(segment);
        }

        // GET: MySegment/Delete/5
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

        // POST: MySegment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var segment = await _context.Segments.FindAsync(id);
            _context.Segments.Remove(segment);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Redirect($"http://localhost:5000/MySegment/Index/{segment.GameId}");
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.Id == id);
        }
    }
}
