using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace GeekyTool.Extensions
{
    public static class StorageFileExtensions
    {
        public static async Task<byte[]> ToByteArrayAsync(this StorageFile file)
        {
            using (var stream = await file.OpenReadAsync())
            {
                using (var reader = new DataReader(stream.GetInputStreamAt(0)))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    var fileBytes = new byte[stream.Size];
                    reader.ReadBytes(fileBytes);
                    return fileBytes;
                }
            }
        }

        public static async Task<BitmapImage> ToBitmapImageAsync(this StorageFile file)
        {
            var src = new BitmapImage();
            src.SetSource(await file.OpenReadAsync());
            return src;
        }

        public static async Task<byte[]> ToResizedImageAsync(this StorageFile file, int maxWidth = 1600,
            int maxHeight = 1200)
        {
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                // Init variables
                byte[] byteArray = null;
                var decoder = await BitmapDecoder.CreateAsync(stream);
                var sourceWidth = decoder.PixelWidth;
                var sourceHeight = decoder.PixelHeight;

                // Calculate scaled ratio and dimensions
                var scaleRatio = Math.Min(maxWidth / sourceWidth, maxHeight / sourceHeight);

                var scaledWidth = sourceWidth * scaleRatio;
                var scaledHeight = sourceHeight * scaleRatio;

                var scaledStream = new InMemoryRandomAccessStream();
                var encoder = await BitmapEncoder.CreateForTranscodingAsync(scaledStream, decoder);

                // Transform image and write it to stream
                encoder.BitmapTransform.ScaledWidth = Convert.ToUInt32(scaledWidth);
                encoder.BitmapTransform.ScaledHeight = Convert.ToUInt32(scaledHeight);
                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    switch (err.HResult)
                    {
                        case unchecked((int)0x88982F81): //WINCODEC_ERR_UNSUPPORTEDOPERATION
                            encoder.IsThumbnailGenerated = false;
                            break;
                        default:
                            throw err;
                    }
                }

                // Read bytes from stream and return
                byteArray = new byte[scaledStream.Size];
                var dataReader = new DataReader(scaledStream.GetInputStreamAt(0));
                await dataReader.LoadAsync((uint)scaledStream.Size);

                dataReader.ReadBytes(byteArray);

                return byteArray;
            }
        }

        public static async Task<StorageFile> ToLocalFileAsync(this StorageFile file, string fileName, string folder = "images")
        {
            var imagesFolder =
                await
                    ApplicationData.Current.LocalFolder.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);

            var localfile =
                await
                    file.CopyAsync(imagesFolder, $"{fileName}{file.FileType}",
                        NameCollisionOption.ReplaceExisting);

            return localfile;
        }
    }
}
