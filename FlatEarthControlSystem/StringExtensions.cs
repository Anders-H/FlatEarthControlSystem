using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FlatEarthControlSystem
{
    public static class StringExtensions
    {
        public static string MiddleTrim(this string me)
        {
            if (string.IsNullOrWhiteSpace(me))
                return "";
            
            me = me.Trim();
            
            var parts = Regex.Split(me, @"\s+");
            
            return string.Join(' ', parts);
        }

        public static string OnlyBasicCharacters(this string me)
        {
            if (string.IsNullOrWhiteSpace(me))
                return "";
            
            var result = new StringBuilder();

            foreach (var c in me.Where(c => "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".IndexOf(c) >= 0))
                result.Append(c);
            
            return result.ToString();
        }
    }
}