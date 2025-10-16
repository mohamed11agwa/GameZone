using GameZone.Attributes;
using GameZone.CustomAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel:GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtensions(allowedExtensions: FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;

    }
}
