using System.Linq;
using System.Text;

namespace FlatEarthControlSystem
{
    public static class StringExtensions
    {
        public static string OnlyBasicCharacters(this string me)
        {
            if (string.IsNullOrWhiteSpace(me))
                return "";
            
            var result = new StringBuilder();

            foreach (var c in me.Where(c => "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".IndexOf(c) >= 0))
                result.Append(c);
            
            return result.ToString();
        }

        public static bool IsSingleWord(this string me) =>
            me.IndexOf(' ') < 0;
    }
}