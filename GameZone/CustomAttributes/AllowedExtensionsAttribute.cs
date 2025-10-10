namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute: ValidationAttribute
    {
        private readonly string AllowedExtensions;
        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            AllowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file == null)
            {
                return new ValidationResult("Please select a file.");
            }
            var extension = Path.GetExtension(file.FileName);
            var IsAllowed = AllowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
            if(!IsAllowed)
            {
                return new ValidationResult($"This file extension is not allowed. Allowed extensions are: {AllowedExtensions}");
            }
            return ValidationResult.Success;
        }
    }
}
