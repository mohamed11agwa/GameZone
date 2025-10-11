using System.Diagnostics;
using GameZone.IRepository;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepo GameRepo;
        public HomeController(ILogger<HomeController> logger, IGameRepo gameRepo)
        {
            _logger = logger;
            GameRepo = gameRepo;
        }


        public IActionResult Index()
        {
            var games = GameRepo.GetAll();
            return View(games);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
