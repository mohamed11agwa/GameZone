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

        public IEnumerable<Game> GetAll()
        {
            var games = Context.Games.Include(g => g.Category).Include(g => g.Devices).AsNoTracking().ToList();
            return games;
        }


        public Game? GetById(int id)
        {
            var game = Context.Games.Include(g => g.Category).Include(g => g.Devices)
                                .AsNoTracking().SingleOrDefault(g => g.Id == id);
            return game;
        }


        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = await SaveCover(model.Cover);
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

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = Context.Games.Include(g => g.Devices).SingleOrDefault(g => g.Id == model.Id);
            if (game == null) 
                return null;
            var hasNewCover = model.Cover != null;
            var OldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = Context.Devices.Where(d => model.SelectedDevices.Contains(d.Id)).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = Context.SaveChanges();
            if(effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var Cover = Path.Combine(imagesPath, OldCover);
                    File.Delete(Cover);
                }
                return game;
            }else
             {
                var Cover = Path.Combine(imagesPath, game.Cover);
                File.Delete(Cover);
                return null;
             }


        }



        public bool Delete(int id)
        {
            bool IsDeleted = false;
            var game = Context.Games.Find(id);
            if(game is null)
                return IsDeleted;
            Context.Remove(game);
            var effectedRows = Context.SaveChanges();
            if (effectedRows > 0)
            {
                IsDeleted = true;
                var path = Path.Combine(imagesPath, game.Cover);
                File.Delete(path);
            }
           
            return IsDeleted;
        }


        private async Task<string> SaveCover(IFormFile Cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(imagesPath, CoverName);
            using var Stream = File.Create(path);
            await Cover.CopyToAsync(Stream);
            return CoverName;
        }


    }
}
