using Windows.UI;
using Windows.UI.Xaml.Media;

namespace GeekyTool.Converters
{
    public class HexToColorConverter
    {
        /// <summary>
        /// Converts a hexadecimal string value into a Brush.
        /// </summary>
        public static Brush Convert(string value)
        {
            byte alpha;
            byte pos = 0;

            string hex = value.ToString().Replace("#", "");

            if (hex.Length == 8)
            {
                alpha = System.Convert.ToByte(hex.Substring(pos, 2), 16);
                pos = 2;
            }
            else
            {
                alpha = System.Convert.ToByte("ff", 16);
            }

            byte red = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            pos += 2;
            byte green = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            pos += 2;
            byte blue = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            return new SolidColorBrush(Color.FromArgb(alpha, red, green, blue));
        }

        /// <summary>
        /// And back again.
        /// </summary>
        public static string ConvertBack(SolidColorBrush val)
        {
            return "#" + val.Color.A.ToString() + val.Color.R.ToString() + val.Color.G.ToString() + val.Color.B.ToString();
        }
    }
}
