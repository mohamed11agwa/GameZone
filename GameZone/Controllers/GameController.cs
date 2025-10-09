using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext Context;

        public GameController(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Categories"] = Context.Categories.ToList();
            CreateGameFormViewModel ViewModel = new CreateGameFormViewModel()
            {
                Categories = Context.Categories.Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).OrderBy(c => c.Text).ToList(),
                Devices = Context.Devices.Select(d => new SelectListItem()
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).OrderBy(d => d.Text).ToList()

            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGameFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = Context.Categories.Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).OrderBy(c => c.Text).ToList();
                model.Devices = Context.Devices.Select(d => new SelectListItem()
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).OrderBy(d => d.Text).ToList();

                return View(model);
            }
            //save Game to database
            //save cover to Server

            return RedirectToAction(nameof(Index));
        }


    }
}
