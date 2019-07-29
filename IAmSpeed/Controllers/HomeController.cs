using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IAmSpeed.Models;

namespace IAmSpeed.Controllers
{
    public class HomeController : Controller
    {
        //public async Task<IActionResult> Index()
        public IActionResult Index () 
        {
            //ApiHelper.InitializeClient();
            //var gameData = await TestClass.LoadData("Super%20Mario%20Bros");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
