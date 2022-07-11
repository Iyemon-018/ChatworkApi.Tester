namespace ChatworkApi.Tester
{
    using System;
    using System.IO;

    public static class Constants
    {
        public static readonly string ProductName = "ChatworkApi.Tester";

        public static readonly string ApplicationDirectory = Path.Combine(Path.GetTempPath(), ProductName);

        public static readonly string ConfigurationFileName = Path.Combine(ApplicationDirectory
                                                                         , "Configs"
                                                                         , "Application.json");

        public static readonly string ImageCacheDirectory = Path.Combine(ApplicationDirectory, "images");

        public static readonly string AccountImageCacheDirectory = Path.Combine(ImageCacheDirectory, "account");

        public static readonly string RoomImageCacheDirectory = Path.Combine(ImageCacheDirectory, "room");
    }
}