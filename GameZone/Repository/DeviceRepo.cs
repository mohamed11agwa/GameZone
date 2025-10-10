using GameZone.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Repository
{
    public class DeviceRepo : IDeviceRepo
    {
        private readonly ApplicationDbContext Context;

        public DeviceRepo(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return Context.Devices.Select(d => new SelectListItem()
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).OrderBy(d => d.Text).ToList();
        }
    }
}
