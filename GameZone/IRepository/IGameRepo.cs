using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.IRepository
{
    public interface IGameRepo
    {
        public IEnumerable<Game> GetAll();
        public Game? GetById(int id);
        public Task Create(CreateGameFormViewModel model);
        public Task<Game?> Edit(EditGameFormViewModel model);
        public bool Delete(int id);
    }
}
