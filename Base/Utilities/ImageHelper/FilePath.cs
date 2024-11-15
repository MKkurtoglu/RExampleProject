namespace Base.Utilities.ImageHelper
{
    public static partial class GuidHelper
    {
        public static class FilePath
        {
            public static string Full(string path, string root = ImageConstEntity.root, string fileType = ImageConstEntity.ek)
            {
                return Path.Combine(Directory.GetCurrentDirectory(), root + fileType, path);
            }
        }
    }
}
