using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace GeekyTool.Extensions
{
    public static class ColorExtensions
    {
        public static SolidColorBrush GetBrushColorFromHexa(this string hexaColor)
        {
            return new SolidColorBrush(
                GetColorFromHexa(hexaColor)
            );
        }

        public static Color GetColorFromHexa(this string hexaColor)
        {
            const string validRegex = @"[#]?[a-zA-Z0-9]{8}|[#]?[a-zA-Z0-9]{6}";

            if (!new Regex(validRegex).IsMatch(hexaColor))
                throw new FormatException("hexaColor incorrect format");

            hexaColor = hexaColor.Replace("#", string.Empty);

            if (hexaColor.Length == 8)
            {
                return Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(0, 2), 16),
                    Convert.ToByte(hexaColor.Substring(2, 2), 16),
                    Convert.ToByte(hexaColor.Substring(4, 2), 16),
                    Convert.ToByte(hexaColor.Substring(6, 2), 16)
                );
            }

            return Color.FromArgb(
                255,
                Convert.ToByte(hexaColor.Substring(0, 2), 16),
                Convert.ToByte(hexaColor.Substring(2, 2), 16),
                Convert.ToByte(hexaColor.Substring(4, 2), 16)
            );
        }

        public static async Task<Color> GetDominantColor(this StorageFile file)
        {
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                //Create a decoder for the image
                var decoder = await BitmapDecoder.CreateAsync(stream);

                //Create a transform to get a 1x1 image
                var myTransform = new BitmapTransform { ScaledHeight = 1, ScaledWidth = 1 };

                //Get the pixel provider
                var pixels = await decoder.GetPixelDataAsync(
                    BitmapPixelFormat.Rgba8,
                    BitmapAlphaMode.Ignore,
                    myTransform,
                    ExifOrientationMode.IgnoreExifOrientation,
                    ColorManagementMode.DoNotColorManage);

                //Get the bytes of the 1x1 scaled image
                var bytes = pixels.DetachPixelData();

                //read the color 
                return Color.FromArgb(255, bytes[0], bytes[1], bytes[2]);
            }
        }

        public static Color InvertColor(this string value)
        {
            if (value != null)
            {
                var ColorToConvert = GetColorFromHexa(value);
                var invertedColor = Color.FromArgb(255, (byte)~ColorToConvert.R, (byte)~ColorToConvert.G, (byte)~ColorToConvert.B);
                return invertedColor;
            }
            else
            {
                return new Color();
            }
        }
    }
}
