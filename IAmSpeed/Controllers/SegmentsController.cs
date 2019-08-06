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
        [Authorize]
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

                foreach(var ucgl in userCurrentGameList)
                {
                    listGameBase.games.Add(ucgl); 
                }

                return View(listGameBase);         
            }
        }


        // GET: Segments/Details/5
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Create(string id)
        {
            var currentUser = await GetCurrentUserAsync();

            var gameCheck = await _context.Games
                .Where(g => g.UserId == currentUser.Id)
                .FirstOrDefaultAsync(g => g.GameIdFromAPI == id);

            if(gameCheck == null)
            {

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
        [Authorize]
        public async Task<IActionResult> Create(GameSegmentViewModel gameSegment)
        {
            var currentUser = await GetCurrentUserAsync();

            GameSegmentViewModel createSegment = new GameSegmentViewModel();

            createSegment.Segment = gameSegment.Segment;

            createSegment.Segment.GameId = gameSegment.Game.Id;

            createSegment.Segment.UserId = currentUser.Id;

            createSegment.Segment.GameIdFromAPI = gameSegment.Game.GameIdFromAPI;
            //createSegment.Segment.OrginId = gameSegment.Segment.Id;

            
            if (ModelState.IsValid)
            {
                _context.Add(createSegment.Segment);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var setOrgin = _context.Segments;

                return Redirect($"http://localhost:5000/MySegment/Index/{gameSegment.Game.Id}");


            }
            //ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", segment.GameId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", segment.UserId);
            return View(createSegment.Segment);
        }

        // GET: Segments/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameSegmentViewModel gameSegment = new GameSegmentViewModel();

            var segments = _context.Segments
                .Where(s => s.GameId == id).ToList();

            gameSegment.segmentsList = segments;

            var game = await _context.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            gameSegment.Game = game; 

            if (game == null)
            {
                return NotFound();
            }

            return View(gameSegment);
        }

        // POST: Segments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.Id == id);
        }
    }
}
