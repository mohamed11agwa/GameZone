using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.IRepository
{
    public interface ICategoryRepo
    {
        public IEnumerable<SelectListItem> GetSelectList();
    }
}
