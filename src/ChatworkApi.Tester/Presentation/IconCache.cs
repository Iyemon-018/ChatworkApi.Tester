namespace ChatworkApi.Tester.Presentation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    internal static class IconCache
    {
        private static readonly ConcurrentDictionary<string, BitmapImage> AccountIconCache =
            new ConcurrentDictionary<string, BitmapImage>();

        private static readonly ConcurrentDictionary<string, BitmapImage> RoomIconCache =
            new ConcurrentDictionary<string, BitmapImage>();

        private static readonly ImageConverter _imageConverter = new ImageConverter();

        /// <summary>
        /// 拡張子に対するイメージのフォーマット
        /// </summary>
        private static readonly ConcurrentDictionary<string, ImageFormat> ExtensionToImageFormatCache
            = new ConcurrentDictionary<string, ImageFormat>(new Dictionary<string, ImageFormat>
                                                            {
                                                                {".png", ImageFormat.Png}
                                                              , {".jpg", ImageFormat.Jpeg}
                                                              , {".jpeg", ImageFormat.Jpeg}
                                                            });

        /// <summary>
        /// ユーザーアカウントのアイコン取得のための排他制御用ロック
        /// </summary>
        private static readonly SemaphoreSlim _accountIconLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// ルームアカウントのアイコン取得のための排他制御用ロック
        /// </summary>
        private static readonly SemaphoreSlim _roomIconLock = new SemaphoreSlim(1, 1);

        static IconCache()
        {
        }

        /// <summary>
        /// ファイルのURLからユーザー アカウントのアバターアイコンを非同期で取得します。
        /// </summary>
        /// <param name="id">アカウントID</param>
        /// <param name="path">アバター アイコンURL</param>
        /// <returns></returns>
        public static async Task<BitmapImage> ToAccountIcon(int    id
                                                          , string path)
        {
            _accountIconLock.Wait();

            try
            {
                if (string.IsNullOrWhiteSpace(path)) return null;

                // キャッシュに乗っていればそっちを使用することで高速化を図る。
                if (AccountIconCache.ContainsKey(path)) return AccountIconCache[path];

                var imageDirectory = Path.Combine(Constants.AccountImageCacheDirectory, id.ToString());
                if (!Directory.Exists(imageDirectory)) Directory.CreateDirectory(imageDirectory);

                var imageFilePath = Path.Combine(imageDirectory, Path.GetFileName(path));
                var image         = await DownloadImageAsync(path, imageFilePath).ConfigureAwait(false);
                AccountIconCache.TryAdd(path, image);

                return image;
            }
            finally
            {
                _accountIconLock.Release();
            }
        }

        /// <summary>
        /// アイコンURLからファイルをダウンロードします。
        /// </summary>
        /// <param name="iconUrl">アイコンURL</param>
        /// <returns></returns>
        private static async Task<byte[]> DownloadImageAsync(string iconUrl)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadDataTaskAsync(iconUrl).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// アイコンURLからファイルをダウンロードしてローカルストレージにダウンロードします。
        /// </summary>
        /// <param name="iconUrl">アイコンURL</param>
        /// <param name="saveFileName">保存先のファイル名</param>
        /// <returns></returns>
        private static async Task DownloadAndSaveImageAsync(string iconUrl
                                                          , string saveFileName)
        {
            var imageBinary = await DownloadImageAsync(iconUrl).ConfigureAwait(false);
            var image       = _imageConverter.ConvertFrom(imageBinary) as Image;

            var extension = Path.GetExtension(saveFileName);
            var format = ExtensionToImageFormatCache.ContainsKey(extension)
                             ? ExtensionToImageFormatCache[extension]
                             : ImageFormat.Jpeg;
            image.Save(saveFileName, format);
        }

        /// <summary>
        /// アイコンをダウンロードし、<see cref="BitmapImage" /> へ変換します。
        /// </summary>
        /// <param name="iconUrl">アイコンURL</param>
        /// <param name="saveFileName">保存先のファイル名</param>
        /// <returns></returns>
        private static async Task<BitmapImage> DownloadImageAsync(string iconUrl
                                                                , string saveFileName)
        {
            if (!File.Exists(saveFileName))
            {
                await DownloadAndSaveImageAsync(iconUrl, saveFileName).ConfigureAwait(false);
            }

            return await Task.Run(() =>
                                  {
                                      var image = new BitmapImage();
                                      image.BeginInit();
                                      image.CacheOption   = BitmapCacheOption.OnLoad;
                                      image.CreateOptions = BitmapCreateOptions.None;
                                      image.UriSource     = new Uri(saveFileName);
                                      image.EndInit();
                                      image.Freeze();

                                      return image;
                                  })
                             .ConfigureAwait(false);
        }

        public static async Task<BitmapImage> ToRoomIcon(int    id
                                                       , string path)
        {
            _roomIconLock.Wait();

            try
            {
                // キャッシュに乗っていればそっちを使用することで高速化を図る。
                if (RoomIconCache.ContainsKey(path)) return RoomIconCache[path];

                var imageDirectory = Path.Combine(Constants.RoomImageCacheDirectory, id.ToString());
                if (!Directory.Exists(imageDirectory)) Directory.CreateDirectory(imageDirectory);

                var imageFilePath = Path.Combine(imageDirectory, Path.GetFileName(path));
                var image         = await DownloadImageAsync(path, imageFilePath).ConfigureAwait(false);
                RoomIconCache.TryAdd(path, image);

                return image;
            }
            finally
            {
                _roomIconLock.Release();
            }
        }
    }
}