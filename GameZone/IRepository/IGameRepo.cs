using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.IRepository
{
    public interface IGameRepo
    {
        public IEnumerable<Game> GetAll();
        public Task Create(CreateGameFormViewModel model);
    }
}
