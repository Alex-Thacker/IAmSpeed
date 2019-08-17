using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAmSpeed.Data;
using IAmSpeed.Models;
using IAmSpeed.Models.GameViewModels;
using IAmSpeed.Models.LeaderUserViewModels;

namespace IAmSpeed.Controllers
{
    public class LeaderRunsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderRunsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaderRuns
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
        }

        // GET: LeaderRuns/Details/5
        public async Task<IActionResult> Details(string id)
        {
            //LeaderUserView leaderUserView = new LeaderUserView();

            var url = $"https://www.speedrun.com/api/v1/categories/{id}/records?top=10";
            var records = await TestClass.GetRecords(url);

            //leaderUserView.leaderBase = records;

            //foreach(var u in records.data)
            //{
            //    foreach(var p in u.runs)
            //    {
            //        var userUrl = p.run.players.First().uri;
            //        var playerInfo = await TestClass.GetUserInfo(userUrl);
            //        leaderUserView.singlePlayerList.Add(playerInfo);
            //    }
            //}

            return View(records);
        }



        //GET: LeaderRuns/Create
        public async Task<IActionResult> Create(string id)
        {
            if(id != null)
            {
            GameCategoriesViewModel gameCategoriesViewModel = new GameCategoriesViewModel(); 

            var url = $"https://www.speedrun.com/api/v1/games/{id}/categories";
            var categories = await TestClass.GetCategories(url);
            gameCategoriesViewModel.categories = categories; 

            var gameUrl = $"https://www.speedrun.com/api/v1/games/{id}";
            var game = await TestClass.LoadSingleData(gameUrl);
            gameCategoriesViewModel.game = game;

            return View(gameCategoriesViewModel);

            }else
            {
                return View();
            }
        }

        // POST: LeaderRuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(leaderRun);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            return View();
        }



        // GET: LeaderRuns/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var leaderRun = await _context.LeaderRun.FindAsync(id);
            //if (leaderRun == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        // POST: LeaderRuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,weblink,game,category,comment,date,submitted")] LeaderRun leaderRun)
        {
            //if (id != leaderRun.id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(leaderRun);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!LeaderRunExists(leaderRun.id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View();
        }

        // GET: LeaderRuns/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var leaderRun = await _context.LeaderRun
            //    .FirstOrDefaultAsync(m => m.id == id);
            //if (leaderRun == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: LeaderRuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var leaderRun = await _context.LeaderRun.FindAsync(id);
            _context.LeaderRun.Remove(leaderRun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaderRunExists(string id)
        {
            return _context.LeaderRun.Any(e => e.id == id);
        }
    }
}
