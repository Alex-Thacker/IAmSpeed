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
using Microsoft.AspNetCore.Authorization;

namespace IAmSpeed.Controllers
{
    [Authorize]
    public class FindSegmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FindSegmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

       
        public async Task<IActionResult> ShowSegmentsFromSearch(string id)
        {
            var getCurrentUser = await GetCurrentUserAsync();

            var game = _context.Games
                .Where(g => g.UserId == getCurrentUser.Id)
                .FirstOrDefaultAsync(g => g.GameIdFromAPI == id);

            if(game.Result == null)
            {
                var url = $"https://www.speedrun.com/api/v1/games/{id}";

                ApiHelper.InitializeClient();
                var singleGameData = await TestClass.LoadSingleData(url);

                Game newGame = new Game();
                newGame.GameIdFromAPI = singleGameData.data.id;
                newGame.Name = singleGameData.data.names.international;
                newGame.Picture = singleGameData.data.assets.covermedium.uri;
                newGame.ReleaseDate = singleGameData.data.released;
                newGame.UserId = getCurrentUser.Id;

                _context.Add(newGame);
                await _context.SaveChangesAsync();

                GameSegmentViewModel gameSegment = new GameSegmentViewModel();

                gameSegment.Game = newGame;

                var segmentsByGame = await _context.Segments
                .Include(s => s.User)
                .Where(s => s.UserId != getCurrentUser.Id)
                .Where(s => s.GameIdFromAPI == id).ToListAsync();

                gameSegment.segmentsList = segmentsByGame;

                var gameById = await _context.Games
                    .FirstOrDefaultAsync(g => g.GameIdFromAPI == id);

                gameSegment.Game = gameById;

                return View("ShowSegments", gameSegment);

            }
            else
            {

                GameSegmentViewModel gameSegment = new GameSegmentViewModel();
                

                var segmentsByGame = await _context.Segments
                    .Include(s => s.User)
                    .Where(s => s.UserId != getCurrentUser.Id)
                    .Where(s => s.GameIdFromAPI == id).ToListAsync();

                gameSegment.segmentsList = segmentsByGame;

                var gameById = await _context.Games
                    .FirstOrDefaultAsync(g => g.GameIdFromAPI == id);

                gameSegment.Game = gameById;

                return View("ShowSegments", gameSegment);
            }

        }

        public async Task<IActionResult> ShowSegments(string GameIdFromAPI)
        {
            GameSegmentViewModel gameSegment = new GameSegmentViewModel();
            var getCurrentUser = await GetCurrentUserAsync();

            var segmentsByGame = await _context.Segments
                .Include(s => s.User)
                .Where(s => s.UserId != getCurrentUser.Id)
                .Where(s => s.GameIdFromAPI == GameIdFromAPI).ToListAsync();

            gameSegment.segmentsList = segmentsByGame;

            var gameById = await _context.Games
                .FirstOrDefaultAsync(g => g.GameIdFromAPI == GameIdFromAPI);

            gameSegment.Game = gameById;

            

            return View(gameSegment); 
        }


        // GET: FindSegment
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
                var currentUser = await GetCurrentUserAsync();


                var userCurrentGameList = _context.Games
                    .Where(g => g.UserId == currentUser.Id);

                ListGameBase listGameBase = new ListGameBase();

                foreach (var ucgl in userCurrentGameList)
                {
                    listGameBase.games.Add(ucgl);
                }

                return View(listGameBase);
            }
        }

        // GET: FindSegment/Details/5
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

        // GET: FindSegment/Create
        public async Task<IActionResult> Create(int id)
        {
            var getCurrentUser = await GetCurrentUserAsync();
            var segmentToClone = await _context.Segments
                .FirstOrDefaultAsync(s => s.Id == id);

            var userSpecificGame = await _context.Games
                .Where(g => g.UserId == getCurrentUser.Id)
                .FirstOrDefaultAsync(g => g.GameIdFromAPI == segmentToClone.GameIdFromAPI);

            Segment cloneSegment = new Segment();
            cloneSegment.Name = segmentToClone.Name;
            cloneSegment.Description = segmentToClone.Description;
            cloneSegment.VideoLink = segmentToClone.VideoLink;
            cloneSegment.Notes = segmentToClone.Notes;
            cloneSegment.PBTime = segmentToClone.PBTime;
            cloneSegment.RNG = segmentToClone.RNG;
            cloneSegment.Category = segmentToClone.Category;
            cloneSegment.UserId = getCurrentUser.Id;
            cloneSegment.GameId = userSpecificGame.Id;
            cloneSegment.GameIdFromAPI = segmentToClone.GameIdFromAPI;

            _context.Add(cloneSegment);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        // POST: FindSegment/Create
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

        // GET: FindSegment/Edit/5
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

        // POST: FindSegment/Edit/5
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

        // GET: FindSegment/Delete/5
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

        // POST: FindSegment/Delete/5
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
