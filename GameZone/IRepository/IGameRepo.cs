using GameZone.ViewModels;

namespace GameZone.IRepository
{
    public interface IGameRepo
    {
       public Task Create(CreateGameFormViewModel model);
    }
}
