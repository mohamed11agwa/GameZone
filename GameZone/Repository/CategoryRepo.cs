using GameZone.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext Context;

        public CategoryRepo(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
           return Context.Categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).OrderBy(c => c.Text).ToList();
        }


    }
}
