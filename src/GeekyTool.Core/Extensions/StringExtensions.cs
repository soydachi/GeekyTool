using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekyTool.Extensions
{
    public static class StringExtensions
    {
        public static string Clean(this string originalString)
        {
            var removeCharSet = new HashSet<char>("?&^$#@!()+-,:;<>’\'\\/-_*".ToCharArray());
            var sb = new StringBuilder(originalString.Length);

            foreach (var x in originalString.ToCharArray().Where(c => !removeCharSet.Contains(c)))
            {
                sb.Append(x);
            }

            return sb.ToString();
        }
    }
}