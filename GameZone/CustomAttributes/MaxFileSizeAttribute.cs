namespace GameZone.CustomAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int MaxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            MaxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return new ValidationResult("Please select a file.");
            }
            if(file.Length > MaxFileSize)
            {
                return new ValidationResult($"The file size exceeds the maximum allowed size of {MaxFileSize} bytes.");
            }
            return ValidationResult.Success;
        }
    }
}
