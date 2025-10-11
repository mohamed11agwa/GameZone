using GameZone.IRepository;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext Context;
        private readonly ICategoryRepo CategoryRepo;
        private readonly IDeviceRepo DeviceRepo;
        private readonly IGameRepo GameRepo;

        public GameController(ApplicationDbContext _Context, ICategoryRepo _CategoryRepo, IDeviceRepo _DeviceRepo, IGameRepo gameRepo)
        {
            Context = _Context;
            CategoryRepo = _CategoryRepo;
            DeviceRepo = _DeviceRepo;
            GameRepo = gameRepo;
        }
        public IActionResult Index()
        {
            var games = GameRepo.GetAll();
            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Categories"] = Context.Categories.ToList();
            CreateGameFormViewModel ViewModel = new CreateGameFormViewModel()
            {
                Categories = CategoryRepo.GetSelectList(),
                Devices = DeviceRepo.GetSelectList()

            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = CategoryRepo.GetSelectList();
                model.Devices = DeviceRepo.GetSelectList();

                return View(model);
            }
            //save Game to database
            //save cover to Server
            await GameRepo.Create(model);


            return RedirectToAction(nameof(Index));
        }


    }
}
//Call of Duty is a 2003 first-person shooter game developed by Infinity Ward and published by Activision. It is the first installment in the Call of Duty franchise, released on October 29, 2003, for Microsoft Windows.