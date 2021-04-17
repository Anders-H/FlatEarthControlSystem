using MutableStringLibrary;

namespace FlatEarthControlSystem
{
    public static class StringExtensions
    {
        public static string OnlyBasicCharacters(this string me)
        {
            var mutable = new MutableString(me);
            mutable.Modify.LimitToCharacters("ABCDEFGHIJKLMNOPQRSTUVWXY Z0123456789");
            return mutable.Value ?? "";
        }

        public static bool IsSingleWord(this string me) =>
            me.IndexOf(' ') < 0;
    }
}