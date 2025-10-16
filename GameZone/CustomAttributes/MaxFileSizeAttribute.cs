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

            if (file is not null)
            {
                if (file.Length > MaxFileSize)
                {
                    return new ValidationResult($"Maximum allowed size is {MaxFileSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
