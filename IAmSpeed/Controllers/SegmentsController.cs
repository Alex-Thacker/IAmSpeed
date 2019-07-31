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
    public class SegmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SegmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Segments
        public async Task<IActionResult> Index(string gameName, string offset)
        {
            ViewData["gameName"] = gameName; 

            var url = $"https://www.speedrun.com/api/v1/games?name={gameName}";

            if (!String.IsNullOrEmpty(gameName))
            {
            ApiHelper.InitializeClient();
            var gameData = await TestClass.LoadData(url);
                return View(gameData);
            }
            if (!String.IsNullOrEmpty(offset))
            {
                url = offset;
                ApiHelper.InitializeClient();
                var offsetData = await TestClass.LoadData(url);
                return View(offsetData);
            }
            else
            {
                return View();
            }
            //var applicationDbContext = _context.Segments.Include(s => s.Game).Include(s => s.User);
            //return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: Segments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"https://www.speedrun.com/api/v1/games/{id}";

            ApiHelper.InitializeClient();
            var singleGameData = await TestClass.LoadSingleData(url);

            if (singleGameData == null)
            {
                return NotFound();
            }

            return View(singleGameData);
        }

        // GET: Segments/Create
        public async Task<IActionResult> Create(string id)
        {

            var gameCheck = await _context.Games
                .FirstOrDefaultAsync(g => g.GameIdFromAPI == id);

            if(gameCheck == null)
            {
            var currentUser = await GetCurrentUserAsync();

            var url = $"https://www.speedrun.com/api/v1/games/{id}";

            ApiHelper.InitializeClient();
            var singleGameData = await TestClass.LoadSingleData(url);

            Game game = new Game();
            game.GameIdFromAPI = singleGameData.data.id;
            game.Name = singleGameData.data.names.international;
            game.Picture = singleGameData.data.assets.covermedium.uri;
            game.ReleaseDate = singleGameData.data.released;
            game.UserId = currentUser.Id;

            _context.Add(game);
            await _context.SaveChangesAsync();

                GameSegmentViewModel gameSegment = new GameSegmentViewModel();

                gameSegment.Game = game; 

            return View(gameSegment);

            } else
            {



            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");

                GameSegmentViewModel gameSegment = new GameSegmentViewModel();
                gameSegment.Game = gameCheck;

                return View(gameSegment);
            }

           

        }

        // POST: Segments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameSegmentViewModel gameSegment)
        {
            GameSegmentViewModel createSegment = new GameSegmentViewModel();

            createSegment.Segment = gameSegment.Segment;

            ModelState.Remove("GameId");
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                _context.Add(createSegment.Segment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", segment.GameId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", segment.UserId);
            return View(createSegment.Segment);
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
