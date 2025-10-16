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

        public IActionResult Details(int id)
        {
            var game = GameRepo.GetById(id);
            if(game == null)
                return NotFound();
            return View(game);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = GameRepo.GetById(id);
            if(game == null)
                return NotFound();
            EditGameFormViewModel viewModel = new EditGameFormViewModel();
            viewModel.Id = game.Id;
            viewModel.Name = game.Name;
            viewModel.Description = game.Description;
            viewModel.CategoryId = game.CategoryId;
            viewModel.SelectedDevices = game.Devices.Select(d => d.Id).ToList();
            viewModel.Categories = CategoryRepo.GetSelectList();
            viewModel.Devices = DeviceRepo.GetSelectList();
            viewModel.CurrentCover = game.Cover;

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = CategoryRepo.GetSelectList();
                model.Devices = DeviceRepo.GetSelectList();

                return View(model);
            }
            //save Game to database
            //save cover to Server
            
            var game = await GameRepo.Edit(model);

            if (game == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }



        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var isDeleted = GameRepo.Delete(id);

            return isDeleted? Ok(): BadRequest();
        }


    }
}
