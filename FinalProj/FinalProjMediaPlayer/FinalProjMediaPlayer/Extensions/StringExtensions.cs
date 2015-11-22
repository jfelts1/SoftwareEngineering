//     James Felts 2015

using System.Linq;
using System.Text;

namespace FinalProjMediaPlayer.Extensions
{
    public static class StringExtensions
    {
        public static string removeNullTerminater(this string str)
        {
            StringBuilder build = new StringBuilder("");
            foreach (var ch in str)
            {
                if (ch == '\0')
                {
                    return build.ToString();
                }
                build.Append(ch);
            }
            return build.ToString();
        }

        public static string removeControlCharacters(this string str)
        {
            StringBuilder build = new StringBuilder();
            foreach (var ch in str.Where(ch => !char.IsControl(ch)))
            {
                build.Append(ch);
            }
            return build.ToString();
        }
    }
}