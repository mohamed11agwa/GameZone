namespace GameZone.Settings
{
    public class FileSettings
    {
        public const string ImagesPath = "/assets/images/games";
        public const string AllowedExtensions  = ".jpg,.png,.jpeg";
        public const int MaxFileSizeInMB = 1; //1MB
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
