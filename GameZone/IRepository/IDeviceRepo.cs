using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.IRepository
{
    public interface IDeviceRepo
    {
        public IEnumerable<SelectListItem> GetSelectList();
    }
}
