using System.Text.RegularExpressions;

namespace FlatEarthControlSystem
{
    public static class StringExtensions
    {
        public static string MiddleTrim(this string me)
        {
            if (string.IsNullOrWhiteSpace(me))
                return "";
            me = me.Trim().ToUpper();
            var parts = Regex.Split(me, @"\s+");
            return string.Join(' ', parts);
        }
    }
}