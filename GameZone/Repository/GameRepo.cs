using GameZone.IRepository;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using System.Threading.Tasks;

namespace GameZone.Repository
{
    public class GameRepo : IGameRepo
    {
        private readonly ApplicationDbContext Context;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly string imagesPath;

        public GameRepo(ApplicationDbContext _Context, IWebHostEnvironment webHostEnvironment)
        {
            Context = _Context;
            WebHostEnvironment = webHostEnvironment;
            imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }


        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(imagesPath, CoverName);
            using var Stream = File.Create(path);
            await model.Cover.CopyToAsync(Stream);
            Game game = new Game()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Devices = Context.Devices.Where(d => model.SelectedDevices.Contains(d.Id)).ToList()
            };
            Context.Add(game);
            Context.SaveChanges();


        }
    }
}
